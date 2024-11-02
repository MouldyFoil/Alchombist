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
    [SerializeField] string[] ignoreTags;
    bool canTarget;
    Vector3 target;
    Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<SavePrepBuffs>())
        {
            FindObjectOfType<SavePrepBuffs>().buffPlayer = true;
        }
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

    private void UpdateTarget()
    {
        List<GameObject> enemiesGameObject = GameObject.FindGameObjectsWithTag("TargetableByPlayer").ToList();
        List<Transform> enemies = new List<Transform>();
        foreach(GameObject enemy in enemiesGameObject)
        {
            enemies.Add(enemy.transform);
        }
        enemies = CleanBlockedEnemies(enemies);
        if (enemies != null && enemies.Count > 0)
        {
            target = GetClosestEnemy(enemies).position;
        }
        if(enemies == null || enemies.Count == 0)
        {
            target = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        }
        canTarget = Vector3.Distance(target, transform.position) < enemyDetectRadius;
    }
    private List<Transform> CleanBlockedEnemies(List<Transform> enemies)
    {
        List<Transform> cleanedEnemies = new List<Transform>();
        foreach(Transform enemy in enemies)
        {
            if (EnemyBlockedCheck(enemy) == false)
            {
                cleanedEnemies.Add(enemy);
            }
        }
        return cleanedEnemies;
    }
    private bool EnemyBlockedCheck(Transform enemy)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, enemy.position - transform.position);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("TargetableByPlayer"))
            {
                return false;
            }
            /*foreach (string tag in ignoreTags)
            {
                Debug.Log(tag);
                if (!hit.collider.gameObject.CompareTag(tag))
                {
                    return true;
                }
            }*/
            if (!hit.collider.gameObject.CompareTag("TargetableByPlayer") && !hit.collider.gameObject.CompareTag(ignoreTags[0]) &&!hit.collider.gameObject.CompareTag(ignoreTags[1]))
            {
                return true;
            }
        }
        return true;
    }
    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform transformMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.position, currentPos);
            if (dist < minDist)
            {
                transformMin = enemy;
                minDist = dist;
            }
        }
        return transformMin;
    }
    private void AimAtTarget()
    {
        target.z = transform.position.z;
        aimDirection = target - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void MoveMarker()
    {
        if (canTarget == true)
        {
            marker.GetComponent<SpriteRenderer>().enabled = true;
            marker.position = new Vector3(target.x, target.y, transform.position.z);
        }
        else
        {
            marker.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public bool ReturnCanTarget() { return canTarget; }
    public Vector3 ReturnTargetLocation() { return target; }
}
