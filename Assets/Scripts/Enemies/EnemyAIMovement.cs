using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIMovement : MonoBehaviour
{
    [SerializeField] float detectDistance = 10;
    [SerializeField] float chaseDistance = 20;
    [SerializeField] float minPathDistance = 6;
    [SerializeField] float stopZoneMin = 3;
    [SerializeField] float stopZoneMax = 5;
    [SerializeField] float speed = 20f;
    //[SerializeField] float avoidOtherEnemyDistance = 2;
    [SerializeField] float minTimeToSwitchDirection = 1;
    [SerializeField] float maxTimeToSwitchDirection = 4;
    [SerializeField] bool circleInStopZone;
    [SerializeField] bool agressiveEnemy = false;
    bool playerRemembered = false;
    bool isSwitchingDirection = false;
    float distanceFromPlayer = Mathf.Infinity;
    bool circleClockwise = true;
    EnemyMovement movement;
    //EnemyAIMovement[] otherEnemies;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
        if (playerRemembered == false && distanceFromPlayer < detectDistance && movement.ReturnIsBlocked() == false)
        {
            playerRemembered = true;
        }
        else if(distanceFromPlayer < minPathDistance && movement.ReturnIsBlocked() == false)
        {
            HandleCombatMovement();
            movement.TogglePathfinding(false);
        }
        else if (playerRemembered == true && distanceFromPlayer < chaseDistance)
        {
            movement.NavigateToPlayer(speed);
        }
        else
        {
            movement.ResetVelocity();
            playerRemembered = false;
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
            if(circleInStopZone == true)
            {
                HandleCircling();
            }
            else
            {
                movement.ResetVelocity();
            }
        }
        if (distanceFromPlayer < stopZoneMin)
        {
            movement.MoveTowardsOrBack(-speed);
        }
    }
    private void HandleCircling()
    {
        AvoidOtherEnemiesCircling();
        if(circleClockwise == true)
        {
            movement.Circle(speed);
        }
        else
        {
            movement.Circle(-speed);
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
    private void AvoidOtherEnemiesCircling()
    {
        //if(otherEnemies != null)
        //{
        //    float currentDistance = Vector3.Distance(GetClosestEnemy().transform.position, transform.position);
        //    if (GetClosestEnemy().ReturnIsSwitching() == false)
        //    {
        //        isSwitchingDirection = true;
        //        if (currentDistance < avoidOtherEnemyDistance)
        //        {
        //            circleClockwise = !circleClockwise;
        //        }
        //        isSwitchingDirection = false;
        //    }
        //}
    }
    //EnemyAIMovement GetClosestEnemy()
    //{
    //    float minDist = Mathf.Infinity;
    //    EnemyAIMovement closestEnemy = null;
    //    Vector3 currentPos = transform.position;
    //    foreach (EnemyAIMovement enemy in otherEnemies)
    //    {
    //        float dist = Vector3.Distance(enemy.transform.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            closestEnemy = enemy;
    //            minDist = dist;
    //        }
    //    }
    //    return closestEnemy;
    //}
    public bool ReturnIsInAttackRange() 
    { 
        if (agressiveEnemy) { return distanceFromPlayer < stopZoneMax; }
        else {return distanceFromPlayer > stopZoneMin && distanceFromPlayer < stopZoneMax;}
    }
}
