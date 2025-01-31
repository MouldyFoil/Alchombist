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
    List<string> movementInputs;

    float offsetOnMove;
    float cornerGraceTime;
    float xGraceTimer = 0;
    float yGraceTimer = 0;
    Vector2 markerPosOffset;
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
            PlayerMoveAim moveAim = FindObjectOfType<PlayerMoveAim>();
            movementInputs = moveAim.movementInputs;
            offsetOnMove = moveAim.offsetOnMove;
            cornerGraceTime = moveAim.cornerGraceTime;
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
            mainScript.marker.position = new Vector3(target.x, target.y, transform.position.z);
        }
        else
        {
            mainScript.marker.GetComponent<SpriteRenderer>().enabled = false;
            HandleNoTargetMarker();
        }
        if (xGraceTimer > 0)
        {
            xGraceTimer -= Time.deltaTime;
        }
        if (yGraceTimer > 0)
        {
            yGraceTimer -= Time.deltaTime;
        }
    }
    //I dont know why I didnt just export the movement aim capabilities to a new script with public funtions but I did this and it works I guess
    private void HandleNoTargetMarker()
    {
        if (movementInputs.Count == 0)
        {
            movementInputs = FindObjectOfType<PlayerMoveAim>().movementInputs;
            if(movementInputs.Count == 0)
            {
                return;
            }
        }
        if (Input.GetKey(movementInputs[0]))
        {
            VerticalInput(true);
        }
        if (Input.GetKey(movementInputs[1]))
        {
            VerticalInput(false);
        }
        if (Input.GetKey(movementInputs[2]))
        {
            HorizontalInput(false);
        }
        if (Input.GetKey(movementInputs[3]))
        {
            HorizontalInput(true);
        }
    }
    private void HorizontalInput(bool right)
    {
        float modifiedOffset = offsetOnMove;
        if (right == false)
        {
            modifiedOffset = -offsetOnMove;
        }
        if (yGraceTimer <= 0)
        {
            markerPosOffset.y = 0;
        }
        xGraceTimer = cornerGraceTime;
        markerPosOffset.x = modifiedOffset;
        mainScript.marker.localPosition = new Vector3(markerPosOffset.x, markerPosOffset.y, transform.position.z);
    }

    private void VerticalInput(bool up)
    {
        float modifiedOffset = offsetOnMove;
        if (up == false)
        {
            modifiedOffset = -offsetOnMove;
        }
        if (xGraceTimer <= 0)
        {
            markerPosOffset.x = 0;
        }
        yGraceTimer = cornerGraceTime;
        markerPosOffset.y = modifiedOffset;
        mainScript.marker.localPosition = new Vector3(markerPosOffset.x, markerPosOffset.y, transform.position.z);
    }
    public bool ReturnCanTarget() { return canTarget; }
    public Vector3 ReturnTargetLocation() { return target; }
}
