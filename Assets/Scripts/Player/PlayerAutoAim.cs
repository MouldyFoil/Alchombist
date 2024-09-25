using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAutoAim : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] Transform marker;
    [SerializeField] float enemyDetectRadius = 20;
    bool canTarget;
    Vector3 target;
    Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        if(canTarget == true)
        {
            AimAtTarget();
        }
        MoveMarker();
    }

    private void MoveMarker()
    {
        if(canTarget == true)
        {
            marker.GetComponent<SpriteRenderer>().enabled = true;
            marker.position = new Vector3(target.x, target.y, transform.position.z);
        }
        else
        {
            marker.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void UpdateTarget()
    {
        Transform[] enemies = GameObject.FindGameObjectsWithTag("TargetableByPlayer").Select(player => player.transform).ToArray();
        if (enemies.Length > 0)
        {
            target = GetClosestEnemy(enemies).position;
        }
        if(enemies.Length == 0)
        {
            target = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        }
        canTarget = Vector3.Distance(target, transform.position) < enemyDetectRadius;
    }
    private void AimAtTarget()
    {
        target.z = transform.position.z;
        aimDirection = target - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform transformMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.position, currentPos);
            if (dist < minDist /*&& EnemyBlockedCheck(enemy) == false*/)
            {
                transformMin = enemy;
                minDist = dist;
            }
        }
        return transformMin;
    }
    private bool EnemyBlockedCheck(Transform enemy)
    {
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.position - enemy.position);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag != "TargetableByPlayer" && hit.collider.gameObject.tag != "Player")
            {
                return true;
            }
            if (hit.collider.tag == "TargetableByPlayer")
            {
                return false;
            }
        }
        return true;
    }
    public bool ReturnCanTarget() { return canTarget; }
    public Vector3 ReturnTargetLocation() { return target; }
}
