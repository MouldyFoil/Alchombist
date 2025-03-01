using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGoblinAttack : MonoBehaviour
{
    [SerializeField] GameObject attack;
    //[SerializeField] float backAwaySpeed = 5;
    [SerializeField] float dashSpeed = 20;
    [SerializeField] float stunTimeAfterAttacking = 2;
    [SerializeField] SpriteRenderer sprite;
    EnemyMovement movement;
    EnemyAttackGeneral attackGeneral;
    bool chargingUp;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        attackGeneral = GetComponent<EnemyAttackGeneral>();
    }

    public void AttackBehavior()
    {
        Debug.Log("attacked");
        movement.Dash(dashSpeed);
        sprite.enabled = false;
        movement.ToggleAim(false);
        attack.SetActive(true);
        StartCoroutine(HandleStun());
    }
    private IEnumerator HandleStun()
    {
        yield return new WaitForSeconds(stunTimeAfterAttacking);
        sprite.enabled = true;
        attack.SetActive(false);
        movement.ToggleAim(true);
        attackGeneral.StartCooldown();
    }
}
