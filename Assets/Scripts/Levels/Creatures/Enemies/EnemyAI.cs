using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float detectDistance = 10;
    [SerializeField] float chaseDistance = 20;
    [SerializeField] float minPathDistance = 6;
    [SerializeField] float stopZoneMin = 3;
    [SerializeField] float stopZoneMax = 5;
    [SerializeField] float stopZoneOffset = 0.5f;
    [SerializeField] float speed = 20f;

    //variables for failed anti-jittering code
    //[SerializeField] float dodgeSpeed = 10f;
    //[SerializeField] float dodgeCooldown = 5f;
    //[SerializeField] float avoidOtherEnemyDistance = 2;


    [SerializeField] float distanceOfCanCircleCheck = 2; //find a better name for this
    [SerializeField] float minTimeToSwitchDirection = 1;
    [SerializeField] float maxTimeToSwitchDirection = 4;
    [SerializeField] bool circleInStopZone;
    [SerializeField] bool agressiveEnemy = false;
    bool playerRemembered = false;
    bool isSwitchingDirection = false;
    float distanceFromPlayer = Mathf.Infinity;
    bool circleClockwise = true;
    EnemyMovement movement;
    FaceTowardsAim stareScript;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (stareScript)
        {
            stareScript.enabled = playerRemembered;
        }
        CalculateDistance();
        if(playerRemembered == false || movement.ReturnIsBlocked() == true)
        {
            movement.PutAwayWeapon(false);
        }
        else
        {
            movement.PutAwayWeapon(true);
        }
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
            //more commented failed anti-jittering code
            //movement.ResetVelocity();
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
            movement.Circle(speed);
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
    public void SetPlayerRemembered(bool remembered) { playerRemembered = remembered; }
    public bool ReturnPlayerRemembered() { return playerRemembered; }
}
