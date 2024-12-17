using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Health>() != null)
        {
            collision.GetComponent<Health>().AddOrRemoveGeneralHealth(-damage);
        }
    }
}
