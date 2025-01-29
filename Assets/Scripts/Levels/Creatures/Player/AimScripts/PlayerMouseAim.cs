using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseAim : MonoBehaviour
{
    PlayerAimMain mainScript;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        mainScript = GetComponent<PlayerAimMain>();
        mainScript.SetMarkerVisibility(true);
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
