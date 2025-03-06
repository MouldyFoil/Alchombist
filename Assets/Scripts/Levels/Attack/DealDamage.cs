using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
    [SerializeField] float timeBetweenHits = 1;
    [SerializeField] int damage;
    [SerializeField] UnityEvent parryEvent;
    [SerializeField] int parryHeal = 1;
    [SerializeField] Transform aim;
    public int parryType;
    float hitClock;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void Update()
    {
        if(hitClock > 0)
        {
            hitClock -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        hitClock = 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(hitClock <= 0)
        {
            hitClock = timeBetweenHits;
            ParryScript parryScript = null;
            if (collision.GetComponentInChildren<ParryScript>())
            {
                parryScript = collision.GetComponentInChildren<ParryScript>();
            }
            if (parryScript != null && parryScript.ReturnParryActive() && parryScript.ReturnParryType() == parryType)
            {
                collision.GetComponent<Health>().AddOrRemoveGeneralHealth(parryHeal);
                rb.velocity = -rb.velocity;
                aim.eulerAngles = new Vector3(aim.eulerAngles.x, aim.eulerAngles.y, aim.eulerAngles.z + 180);
                parryScript.ParrySuccessFX();
                parryEvent.Invoke();
            }
            else if (collision.GetComponent<Health>() != null)
            {
                collision.GetComponent<Health>().AddOrRemoveGeneralHealth(-damage);
            }
        }
    }
}
