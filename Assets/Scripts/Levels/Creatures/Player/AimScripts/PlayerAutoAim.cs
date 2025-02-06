using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAutoAim : MonoBehaviour
{
    [SerializeField] float enemyDetectRadius = 20;
    [SerializeField] string[] ignoreTags;
    PlayerAimMain mainScript;
    bool canTarget;
    Vector3 target;

    PlayerMoveAim moveAim;
    // Start is called before the first frame update
    void Start()
    {
        mainScript = GetComponent<PlayerAimMain>();
        if (FindObjectOfType<SavePrepBuffs>())
        {
            FindObjectOfType<SavePrepBuffs>().buffPlayer = true;
        }
        if (FindObjectOfType<PlayerMoveAim>())
        {
            moveAim = FindObjectOfType<PlayerMoveAim>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
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
            bool ignoreObject = false;
            foreach(string tag in ignoreTags)
            {
                if(hit.collider.gameObject.tag == tag)
                {
                    ignoreObject = true;
                    break;
                }
            }
            if(ignoreObject == false && !hit.collider.gameObject.CompareTag("TargetableByPlayer"))
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
    private void MoveMarker()
    {
        if (canTarget == true)
        {
            mainScript.SetMarkerVisibility(true);
            mainScript.marker.position = target;
            moveAim.markerMoveOnInputs = false;
        }
        else
        {
            mainScript.SetMarkerVisibility(false);
            moveAim.markerMoveOnInputs = true;
        }
    }
    public bool ReturnCanTarget() { return canTarget; }
    public Vector3 ReturnTargetLocation() { return target; }
}
