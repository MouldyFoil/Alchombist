using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseAim : MonoBehaviour
{
    PlayerAimMain mainScript;
    Vector3 mousePos;

    PlayerMoveAim moveAim;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<PlayerMoveAim>())
        {
            moveAim = FindObjectOfType<PlayerMoveAim>();
        }
    }
    private void OnEnable()
    {
        if (FindObjectOfType<PlayerMoveAim>())
        {
            moveAim = FindObjectOfType<PlayerMoveAim>();
        }
        mainScript = GetComponent<PlayerAimMain>();
        mainScript.SetMarkerVisibility(true);
        moveAim.markerMoveOnInputs = false;
    }
    void Update()
    {
        if (!mainScript.marker.GetComponent<SpriteRenderer>().enabled)
        {
            mainScript.SetMarkerVisibility(true);
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        mainScript.marker.position = mousePos;
    }
    
}
