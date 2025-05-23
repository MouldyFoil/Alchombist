using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform attackSpawn;
    [SerializeField] float timeBetweenAttack = 0.1f;
    [SerializeField] SpriteRenderer parryIndicator;
    [SerializeField] GameObject projectile;
    [SerializeField] float activationDistance = 10;
    [SerializeField] float parryIndicatorTime = 0.1f;
    [SerializeField] string[] ignoreTags;
    [SerializeField] float blockedCheckDistance = 1;
    [HideInInspector]
    public bool activateOverride;
    bool blockedByPlayerObject;
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
        if(distanceFromPlayer <= activationDistance && !BlockedByObjectCheck())
        {
            if (attackClock <= 0)
            {
                parryIndicateClock = parryIndicatorTime;
                Shoot();
            }
            else if (activateOverride && attackClock <= 0)
            {
                parryIndicateClock = parryIndicatorTime;
                Shoot();
            }
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
    private bool BlockedByObjectCheck()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.right, blockedCheckDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("PlayerObject"))
            {
                return true;
            }
        }
        return false;
    }
}
