using Pathfinding;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection and pathfinding stuffs")]
    [SerializeField] float detectDistance = 10;
    [SerializeField] float chaseDistance = 20;
    [SerializeField] float minPathDistance = 6;
    [SerializeField] float forgetTime = 2;
    float forgetTimer;
    [Header("Stopzone stuffs")]
    [SerializeField] float stopZoneMin = 3;
    [SerializeField] float stopZoneMax = 5;
    //[SerializeField] float stopZoneOffset = 0.5f;
    [Header("Circling stuffs")]
    [SerializeField] bool circleInStopZone;
    [SerializeField] float distanceOfCanCircleCheck = 2; //find a better name for this
    [SerializeField] float minTimeToSwitchDirection = 1;
    [SerializeField] float maxTimeToSwitchDirection = 4;

    [Header("Movement things")]
    [SerializeField] float speed = 20f;
    /*[SerializeField] float dodgeSpeed = 30f;
    [SerializeField] float timeBetweenDodges = 15f;
    [SerializeField] float durationOfDodge = 1f;
    [SerializeField] bool canDodgeAttacks = true;*/

    [SerializeField] bool agressiveEnemy = false;
    //bool cornered; //unimplimented bool: it would be funny if non-agressive enemies started sweating or something when they were cornered
    [SerializeField] int[] sightBlockLayers;
    [SerializeField] float circleSwitchCooldownOnSightBlock = 1;
    float distanceFromPlayer = Mathf.Infinity;
    bool playerRemembered = false;
    bool isSwitchingDirection = false;
    bool circleClockwise = true;
    EnemyMovement movement;
    EnemyAttackGeneral attack;
    FaceTowardsAim stareScript;
    float blockedRecentlyTimer = 0;
    //EnemyAIMovement[] otherEnemies;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<FaceTowardsAim>())
        {
            stareScript = GetComponent<FaceTowardsAim>();
            stareScript.enabled = false;
        }
        movement = GetComponent<EnemyMovement>();
        attack = GetComponent<EnemyAttackGeneral>();
        forgetTimer = forgetTime;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
        WeaponOutStuff();
        MainAIThings();
        if (stareScript)
        {
            stareScript.enabled = playerRemembered;
        }
        if(blockedRecentlyTimer > 0)
        {
            blockedRecentlyTimer -= Time.deltaTime;
        }
    }
    private void MainAIThings()
    {
        if (!playerRemembered && distanceFromPlayer < detectDistance && ReturnIsBlocked() == false)
        {
            playerRemembered = true;
        }
        else if (distanceFromPlayer < minPathDistance && ReturnIsBlocked() == false)
        {
            HandleCombatMovement();
            movement.TogglePathfinding(false);
        }
        else if (playerRemembered && distanceFromPlayer < chaseDistance)
        {
            movement.NavigateToPlayer(speed);
        }
        else
        {
            //more commented failed anti-jittering code
            //movement.ResetVelocity();
            forgetTimer -= Time.deltaTime;
            if (forgetTimer <= 0)
            {
                playerRemembered = false;
            }
        }
        if (ReturnIsBlocked())
        {
            if (blockedRecentlyTimer <= 0)
            {
                circleClockwise = !circleClockwise;
                blockedRecentlyTimer = circleSwitchCooldownOnSightBlock;
            }
            forgetTimer -= Time.deltaTime;
            if(forgetTimer <= 0)
            {
                playerRemembered = false;
            }
        }
        else
        {
            forgetTimer = forgetTime;
        }
    }
    

    private void WeaponOutStuff()
    {
        if (playerRemembered == false || ReturnIsBlocked() == true)
        {
            movement.PutAwayWeapon(false);
        }
        else
        {
            movement.PutAwayWeapon(true);
        }
    }

    public bool ReturnIsSwitching() { return isSwitchingDirection; }
    private void CalculateDistance()
    {
        distanceFromPlayer = Vector3.Distance(movement.ReturnTarget(), transform.position);
    }
    private void HandleCombatMovement()
    {
        if (distanceFromPlayer > stopZoneMax)
        {
            movement.MoveTowardsOrBack(speed);
        }
        if (distanceFromPlayer > stopZoneMin && distanceFromPlayer < stopZoneMax)
        {
            if(circleInStopZone == true && movement.ReturnCanCircleInSpace(distanceOfCanCircleCheck))
            {
                HandleCircling();
            }
        }
        if (distanceFromPlayer < stopZoneMin)
        {
            movement.MoveTowardsOrBack(-speed);
        }
    }
    private void HandleCircling()
    {
        if(circleClockwise == true)
        {
            movement.Circle(speed);
            // this code is meant to prevent jittering but ended up creating more jittering
            //if (distanceFromPlayer < stopZoneMax - stopZoneOffset)
            //{
            //    movement.MoveDiagonal(speed, speed);
            //}
            //else if (distanceFromPlayer > stopZoneMin + stopZoneOffset)
            //{
            //    movement.MoveDiagonal(-speed, speed);
            //}
            //else
            //{
            //    movement.Circle(speed);
            //}
        }
        else
        {
            movement.Circle(-speed);
            //same with this
            //if (distanceFromPlayer < stopZoneMax - stopZoneOffset)
            //{
            //    movement.MoveDiagonal(speed, -speed);
            //}
            //else if (distanceFromPlayer > stopZoneMin + stopZoneOffset)
            //{
            //    movement.MoveDiagonal(-speed, -speed);
            //}
            //else
            //{
            //    movement.Circle(-speed);
            //}
        }
        if(isSwitchingDirection == false)
        {
            StartCoroutine(HandleRandomSwitching());
        }
    }
    private IEnumerator HandleRandomSwitching()
    {
        isSwitchingDirection = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeToSwitchDirection, maxTimeToSwitchDirection));
        circleClockwise = !circleClockwise;
        isSwitchingDirection = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        circleClockwise = !circleClockwise;
    }
    public bool ReturnIsInAttackRange() 
    { 
        if (agressiveEnemy) { return distanceFromPlayer < stopZoneMax; }
        else {return distanceFromPlayer > stopZoneMin && distanceFromPlayer < stopZoneMax;}
    }
    public bool ReturnIsBlocked()
    {
        List<RaycastHit2D> hits = Physics2D.RaycastAll(transform.position, movement.aimTransform.right).ToList();
        //hits.AddRange(Physics2D.RaycastAll(transform.position, movement.aimTransform.right).ToList());
        foreach (RaycastHit2D hit in hits)
        {
            if (CheckIfLayerBlocks(hit.collider.gameObject.layer))
            {
                return true;
            }
            if (hit.collider.tag == "Player")
            {
                return false;
            }

        }
        return true;
    }
    private bool CheckIfLayerBlocks(int layerIn)
    {
        foreach (int theLayer in sightBlockLayers)
        {
            if (layerIn == theLayer)
            {
                return true;
            }
        }
        return false;
    }
    public void SetPlayerRemembered(bool remembered) { playerRemembered = remembered; }
    public bool ReturnPlayerRemembered() { return playerRemembered; }
}
