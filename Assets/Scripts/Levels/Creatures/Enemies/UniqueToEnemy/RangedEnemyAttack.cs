using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] float recoil = 10;
    [SerializeField] float stunTimeAfterAttacking = 2;
    EnemyMovement movement;
    EnemyAttackGeneral enemyAttackGeneral;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        enemyAttackGeneral = GetComponent<EnemyAttackGeneral>();
    }

    public void AttackBehavior()
    {
        StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        movement.Dash(-recoil);
        Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation * Quaternion.Euler(0, 0, -90));
        yield return new WaitForSeconds(stunTimeAfterAttacking);
        enemyAttackGeneral.StartCooldown();
    }
}
