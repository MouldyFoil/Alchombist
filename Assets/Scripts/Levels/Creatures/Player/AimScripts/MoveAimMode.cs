using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAimMode : MonoBehaviour
{
    PlayerAimMain mainScript;
    PlayerMoveAim moveAim;
    private void OnEnable()
    {
        mainScript = GetComponent<PlayerAimMain>();
        moveAim = GetComponent<PlayerMoveAim>();
        mainScript.SetMarkerVisibility(true);
        moveAim.markerMoveOnInputs = true;
    }
}
