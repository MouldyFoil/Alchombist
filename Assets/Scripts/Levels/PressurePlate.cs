using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] UnityEvent startEvents;
    [SerializeField] UnityEvent pressEvents;
    [SerializeField] UnityEvent stayEvents;
    [SerializeField] UnityEvent exitEvents;
    public bool pressed;
    // Start is called before the first frame update
    private void Start()
    {
        startEvents.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pressEvents.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        exitEvents.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        stayEvents.Invoke();
    }
    public void Press() { pressed = true; }
    public void Unpress() { pressed = false; }
}
