using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] UnityEvent startEvent;
    [SerializeField] UnityEvent pressEvent;
    [SerializeField] UnityEvent unpressEvent;
    [SerializeField] UnityEvent stayEvent;
    [SerializeField] float timeTillUnpressEvents = 0.4f;
    List<GameObject> pressingObjects = new List<GameObject>();
    // Start is called before the first frame update
    private void Start()
    {
        startEvent.Invoke();
    }
    private void Update()
    {
        foreach(GameObject obj in pressingObjects)
        {
            if(obj == null)
            {
                pressingObjects.Remove(obj);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pressingObjects.Count <= 0)
        {
            pressEvent.Invoke();
        }
        if (!ObjectAlreadyPressing(collision.gameObject))
        {
            pressingObjects.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pressingObjects.Remove(collision.gameObject);
        if (pressingObjects.Count <= 0)
        {
            unpressEvent.Invoke();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        stayEvent.Invoke();
    }
    private bool ObjectAlreadyPressing(GameObject saidObject)
    {
        foreach(GameObject thing in pressingObjects)
        {
            if(thing == saidObject)
            {
                return true;
            }
        }
        return false;
    }
}
