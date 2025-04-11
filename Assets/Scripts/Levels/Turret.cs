using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform attackSpawn;
    [SerializeField] float timeBetweenAttack = 0.1f;
    [SerializeField] SpriteRenderer parryIndicator;
    [SerializeField] GameObject projectile;
    [SerializeField] float activationDistance = 10;
    [SerializeField] float parryIndicatorTime = 0.1f;
    float parryIndicateClock;
    float attackClock;
    GameObject player;
    float distanceFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackCooldown();
        IndicateParry();
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer <= activationDistance && attackClock <= 0)
        {
            parryIndicateClock = parryIndicatorTime;
            Shoot();
        }
    }

    private void Shoot()
    {
        attackClock = timeBetweenAttack;
        Instantiate(projectile, attackSpawn);
    }
    private void IndicateParry()
    {
        parryIndicator.enabled = parryIndicateClock > 0;
        if(parryIndicateClock > 0)
        {
            parryIndicateClock -= Time.deltaTime;
        }
    }

    private void HandleAttackCooldown()
    {
        if (attackClock > 0)
        {
            attackClock -= Time.deltaTime;
        }
    }
}
