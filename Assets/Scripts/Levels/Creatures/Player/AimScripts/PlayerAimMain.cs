using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAimMain : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    public Transform marker;
    [SerializeField] string input = "g";
    [SerializeField] MonoBehaviour[] aimScripts;
    [SerializeField] TextMeshProUGUI aimText;
    bool allowSwitching = true;
    int currentAimMode = 0;
    Vector3 aimDirection;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(input) && allowSwitching)
        {
            NextAimMode();
        }
        if (MultipleScriptsEnabledQuery())
        {
            SwitchAimMode();
        }
        AimAtMarker();
    }
    private bool MultipleScriptsEnabledQuery()
    {
        bool oneEnabled = false;
        foreach(MonoBehaviour script in aimScripts)
        {
            if (!oneEnabled && script.enabled)
            {
                oneEnabled = true;
            }
            else if(oneEnabled)
            {
                return true;
            }
        }
        return false;
    }
    private void NextAimMode()
    {
        currentAimMode++;
        if (currentAimMode >= aimScripts.Length)
        {
            currentAimMode = 0;
        }
        SwitchAimMode();
    }
    private void SwitchAimMode()
    {
        int index = 0;
        foreach(MonoBehaviour script in aimScripts)
        {
            if(index == currentAimMode)
            {
                script.enabled = true;
            }
            else
            {
                script.enabled = false;
            }
            index++;
        }
    }
    private void AimAtMarker()
    {
        aimDirection = marker.position - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void SetMarkerVisibility(bool status)
    {
        marker.GetComponent<SpriteRenderer>().enabled = status;
    }
    public int ReturnCurrentModeInt() { return currentAimMode; }
    public void SetCanSwitchAimModes(bool can) { allowSwitching = can; }
}
