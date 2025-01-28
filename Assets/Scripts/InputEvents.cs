using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour
{
    [SerializeField] string inputString;
    [SerializeField] UnityEvent theEvent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputString))
        {
            theEvent.Invoke();
        }
    }
}
