using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float recoil = 10;
    [SerializeField] float stunTimeAfterAttacking = 2;
    [SerializeField] int burstAmount;
    [SerializeField] float timeBetweenShots;
    [SerializeField] bool randomlyChooseProjectiles = false;
    int currentBurst;
    float stunClock;
    float burstClock;
    bool stunned;
    bool attacking = false;
    EnemyMovement movement;
    EnemyAttackGeneral enemyAttackGeneral;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        enemyAttackGeneral = GetComponent<EnemyAttackGeneral>();
        stunClock = stunTimeAfterAttacking;
        currentBurst = burstAmount;
    }

    /*public void AttackBehavior()
    {
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        movement.Dash(-recoil);
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation * Quaternion.Euler(0, 0, -90));
        yield return new WaitForSeconds(stunTimeAfterAttacking);
        enemyAttackGeneral.StartCooldown();
    }*/
    private void Update()
    {
        HandleStunClock();
        HandleBursts();
    }

    private void HandleBursts()
    {
        if (burstClock > 0)
        {
            burstClock -= Time.deltaTime;
        }
        else if(currentBurst < burstAmount)
        {
            attacking = true;
            burstClock = timeBetweenShots;
            movement.Dash(-recoil);
            if (randomlyChooseProjectiles)
            {
                Instantiate(projectiles[Random.Range(0, projectiles.Length)], projectileSpawn.position, projectileSpawn.rotation * Quaternion.Euler(0, 0, -90));
            }
            else
            {
                Instantiate(projectiles[currentBurst], projectileSpawn.position, projectileSpawn.rotation * Quaternion.Euler(0, 0, -90));
            }
            currentBurst++;
        }
        else if (currentBurst == burstAmount && attacking)
        {
            attacking = false;
            StartStun();
        }
    }

    private void HandleStunClock()
    {
        if (stunClock < 0 && stunned == true)
        {
            stunned = false;
            enemyAttackGeneral.StartCooldown();
        }
        else if (stunned == true)
        {
            stunClock -= Time.deltaTime;
            enemyAttackGeneral.enabled = true;
        }
    }

    public void AttackBehavior()
    {
        stunClock = stunTimeAfterAttacking;
        Attack();
        
    }
    private void Attack()
    {
        currentBurst = 0;
    }
    private void StartStun()
    {
        if (stunTimeAfterAttacking > 0)
        {
            stunClock = stunTimeAfterAttacking;
            stunned = true;
        }
        else
        {
            enemyAttackGeneral.StartCooldown();
        }
    }
}
