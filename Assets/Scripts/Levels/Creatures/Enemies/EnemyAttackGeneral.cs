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
    bool charging;
    bool canAttack = true;
    EnemyAI AI;
    EnemyMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAI>();
        movement = GetComponent<EnemyMovement>();
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
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if(AI == true && AI.ReturnIsInAttackRange() == true && movement.ReturnIsBlocked() == false)
        {
            if(charging == false && canAttack == true)
            {
                chargeTimePrime = chargeUpTime;
                charging = true;
                AI.enabled = false;
                movement.TogglePathfinding(false);
            }
        }
    }
    public void StartCooldown()
    {
        cooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        AI.enabled = true;
        canAttack = true;
    }
}
