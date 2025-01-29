using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAim : MonoBehaviour
{
    public float cornerGraceTime = 0.01f;
    public float offsetOnMove = 1;
    PlayerAimMain mainScript;
    [HideInInspector]
    public List<string> movementInputs;
    Vector2 markerPosOffset;
    float yGraceTimer = 0;
    float xGraceTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        markerPosOffset.x = offsetOnMove;
        movementInputs.Add(movementScript.upInput);
        movementInputs.Add(movementScript.downInput);
        movementInputs.Add(movementScript.leftInput);
        movementInputs.Add(movementScript.rightInput);
    }
    private void OnEnable()
    {
        mainScript = GetComponent<PlayerAimMain>();
        mainScript.SetMarkerVisibility(true);
        markerPosOffset.x = 0;
        markerPosOffset.y = -offsetOnMove;
        mainScript.marker.localPosition = new Vector3(markerPosOffset.x, markerPosOffset.y, transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        if (!mainScript.marker.GetComponent<SpriteRenderer>().enabled)
        {
            mainScript.SetMarkerVisibility(true);
        }
        if (xGraceTimer > 0)
        {
            xGraceTimer -= Time.deltaTime;
        }
        if (yGraceTimer > 0)
        {
            yGraceTimer -= Time.deltaTime;
        }
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKey(movementInputs[0]))
        {
            VerticalAiming(true);
        }
        if (Input.GetKey(movementInputs[1]))
        {
            VerticalAiming(false);
        }
        if (Input.GetKey(movementInputs[2]))
        {
            HorizontalAiming(false);
        }
        if (Input.GetKey(movementInputs[3]))
        {
            HorizontalAiming(true);
        }
    }

    private void HorizontalAiming(bool right)
    {
        float modifiedOffset = offsetOnMove;
        if(right == false)
        {
            modifiedOffset = -offsetOnMove;
        }
        if (yGraceTimer <= 0)
        {
            markerPosOffset.y = 0;
        }
        xGraceTimer = cornerGraceTime;
        markerPosOffset.x = modifiedOffset;
        mainScript.marker.localPosition = new Vector3(markerPosOffset.x, markerPosOffset.y, transform.position.z);
    }

    private void VerticalAiming(bool up)
    {
        float modifiedOffset = offsetOnMove;
        if(up == false)
        {
            modifiedOffset = -offsetOnMove;
        }
        if (xGraceTimer <= 0)
        {
            markerPosOffset.x = 0;
        }
        yGraceTimer = cornerGraceTime;
        markerPosOffset.y = modifiedOffset;
        mainScript.marker.localPosition = new Vector3(markerPosOffset.x, markerPosOffset.y, transform.position.z);
    }
}
