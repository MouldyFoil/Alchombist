using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwitch : MonoBehaviour
{
    [SerializeField] string input = "g";
    [SerializeField] PlayerAutoAim autoAimScript;
    [SerializeField] PlayerManualAim manualAimScript;
    bool autoAim = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            autoAim = !autoAim;
            autoAimScript.enabled = autoAim;
            manualAimScript.enabled = !autoAim;
        }
        if (autoAimScript.enabled && manualAimScript.enabled)
        {
            autoAimScript.enabled = autoAim;
            manualAimScript.enabled = !autoAim;
        }
    }
}
