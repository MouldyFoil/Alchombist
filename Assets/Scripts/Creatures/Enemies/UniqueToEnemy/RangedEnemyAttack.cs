using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float minAttackCooldown = 2;
    [SerializeField] float maxAttackCooldown = 7;
    [SerializeField] float recoil = 10;
    [SerializeField] float stunTimeAfterAttacking = 2;
    EnemyAIMovement AI;
    EnemyMovement movement;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAIMovement>();
        movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AI == true && canAttack == true && AI.ReturnIsInAttackRange() == true && movement.ReturnIsBlocked() == false)
        {
            AttackBehavior();
        }
    }

    private void AttackBehavior()
    {
        canAttack = false;
        AI.enabled = false;
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        AI.enabled = false;
        movement.Dash(-recoil);
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation * Quaternion.Euler(0, 0, -90));
        yield return new WaitForSeconds(stunTimeAfterAttacking);
        AI.enabled = true;
        StartCoroutine(HandleCooldown());
    }
    private IEnumerator HandleCooldown()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minAttackCooldown, maxAttackCooldown));
        canAttack = true;
    }
}
