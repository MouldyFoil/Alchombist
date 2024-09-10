using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGoblinAttack : MonoBehaviour
{
    [SerializeField] GameObject attack;
    [SerializeField] float minAttackCooldown = 2;
    [SerializeField] float maxAttackCooldown = 7;
    [SerializeField] float chargeUpTime = 3;
    [SerializeField] float backAwaySpeed = 5;
    [SerializeField] float chargeSpeed = 30;
    [SerializeField] float stunTimeAfterAttacking = 2;
    EnemyAIMovement AI;
    EnemyMovement movement;
    bool canAttack = true;
    bool chargingUp;
    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAIMovement>();
        movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AI == true && canAttack == true && AI.ReturnIsInStopZone() == true)
        {
            AttackBehavior();
        }
        if(chargingUp == true)
        {
            movement.MoveTowardsOrBack(-backAwaySpeed);
        }
    }

    private void AttackBehavior()
    {
        canAttack = false;
        AI.enabled = false;
        StartCoroutine(HandleChargeUp());
    }

    private IEnumerator HandleChargeUp()
    {
        chargingUp = true;
        yield return new WaitForSeconds(chargeUpTime);
        chargingUp = false;
        movement.Dash(chargeSpeed);
        GetComponent<SpriteRenderer>().enabled = false;
        movement.ToggleAim(false);
        attack.SetActive(true);
        StartCoroutine(HandleStun());
    }
    private IEnumerator HandleStun()
    {
        yield return new WaitForSeconds(stunTimeAfterAttacking);
        GetComponent<SpriteRenderer>().enabled = true;
        attack.SetActive(false);
        movement.ToggleAim(true);
        AI.enabled = true;
        StartCoroutine(HandleCooldown());
    }
    private IEnumerator HandleCooldown()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minAttackCooldown, maxAttackCooldown));
        canAttack = true;
    }
}
