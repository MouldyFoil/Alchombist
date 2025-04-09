using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [SerializeField] UnityEvent collisionEvent;
    [SerializeField] string[] tags;
    [SerializeField] bool oneTimeCheck = false;
    bool activated;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!oneTimeCheck || !activated)
        {
            bool theThing = false;
            foreach (string tag in tags)
            {
                if (other.tag == tag)
                {
                    theThing = true;
                }
            }
            if (theThing)
            {
                collisionEvent.Invoke();
            }
            activated = true;
        }
    }
}
