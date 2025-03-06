using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] UnityEvent parryEvent;
    [SerializeField] int parryHeal = 1;
    [SerializeField] Transform aim;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<ParryScript>() && collision.GetComponentInChildren<ParryScript>().ReturnParryActive())
        {
            collision.GetComponent<Health>().AddOrRemoveGeneralHealth(parryHeal);
            rb.velocity = -rb.velocity;
            aim.eulerAngles = new Vector3(aim.eulerAngles.x, aim.eulerAngles.y, aim.eulerAngles.z + 180);
            parryEvent.Invoke();
        }
        else if(collision.GetComponent<Health>() != null)
        {
            collision.GetComponent<Health>().AddOrRemoveGeneralHealth(-damage);
        }
    }
}
