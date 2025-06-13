using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackGeneral : MonoBehaviour
{
    [SerializeField] float minAttackCooldown = 2;
    [SerializeField] float maxAttackCooldown = 7;

    [SerializeField] float chargeUpTime = 2;
    [SerializeField] bool backAwayDuringChargeUp = true;
    [SerializeField] float backAwaySpeed = 2;

    [SerializeField] UnityEvent onAttack;
    float cooldown;
    [SerializeField] float chargeTimePrime;
    [SerializeField] GameObject attackIndicator;
    [SerializeField] float indicatorTime = 0.5f;
    [SerializeField] bool attackRangeOverride = false;
    [SerializeField] bool stunOnAttack = true;
    float indicatorClock;
    bool charging;
    bool canAttack = true;
    EnemyAI AI;
    EnemyMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAI>();
        movement = GetComponent<EnemyMovement>();
        StartCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if(charging == true)
        {
            if (backAwayDuringChargeUp)
            {
                movement.MoveTowardsOrBack(-backAwaySpeed);
            }
            chargeTimePrime -= Time.deltaTime;
            if(chargeTimePrime <= 0)
            {
                canAttack = false;
                onAttack.Invoke();
                charging = false;
            }
        }
        if(indicatorClock > 0)
        {
            indicatorClock -= Time.deltaTime;
        }
        if (attackIndicator)
        {
            attackIndicator.SetActive(indicatorClock > 0);
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if(AI == true && AI.ReturnIsInAttackRange() == true && AI.ReturnIsBlocked() == false)
        {
            if(charging == false && canAttack == true)
            {
                chargeTimePrime = chargeUpTime;
                charging = true;
                Stun();
            }
        }
        else if(attackRangeOverride && AI == true && AI.ReturnPlayerRemembered() && AI.ReturnIsBlocked() == false)
        {
            if(charging == false && canAttack == true)
            {
                chargeTimePrime = chargeUpTime;
                charging = true;
                Stun();
            }
        }
    }

    private void Stun()
    {
        if (stunOnAttack)
        {
            AI.enabled = false;
            movement.TogglePathfinding(false);
        }
    }

    public void IndicateParry()
    {
        indicatorClock = indicatorTime;
    }
    public void StartCooldown()
    {
        cooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        AI.enabled = true;
        canAttack = true;
        movement.enabled = true;
    }
}
