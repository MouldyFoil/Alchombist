using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [SerializeField] int collideRequirement;
    [SerializeField] UnityEvent collisionEvent;
    [SerializeField] string[] tagsForCollision;
    [SerializeField] string[] layersForCollision;
    int collisions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        bool collidedWithProperThing = false;
        foreach(string tag in tagsForCollision)
        {
            if(tag == other.tag)
            {
                collidedWithProperThing = true;
                break;
            }
        }
        foreach (string layer in layersForCollision)
        {
            if (layer == other.tag)
            {
                collidedWithProperThing = true;
                break;
            }
        }
        if (collidedWithProperThing)
        {
            collisions++;
        }
        if(collisions >= collideRequirement)
        {
            collisionEvent.Invoke();
            this.enabled = false;
        }
    }
}
