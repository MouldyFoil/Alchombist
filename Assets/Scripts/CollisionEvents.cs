using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [SerializeField] UnityEvent collisionEvent;
    [SerializeField] string[] tags;
    [SerializeField] int[] layers;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        bool theThing = false;
        foreach (string tag in tags)
        {
            if(other.tag == tag)
            {
                theThing = true;
            }
        }
        foreach (int layer in layers)
        {
            if (other.gameObject.layer == layer)
            {
                theThing = true;
            }
        }
        if (theThing)
        {
            collisionEvent.Invoke();
        }
    }
}
