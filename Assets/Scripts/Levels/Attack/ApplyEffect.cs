using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffect : MonoBehaviour
{
    [SerializeField] GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effect, collision.gameObject.transform);
    }
}
