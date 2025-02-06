using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAim : MonoBehaviour
{
    [SerializeField] float cornerGraceTime = 0.01f;
    [SerializeField] float offsetOnMove = 1;
    [HideInInspector]
    public bool markerMoveOnInputs = false;
    bool markerCanMove = true;
    PlayerAimMain mainScript;
    List<string> movementInputs = new List<string>();
    Vector2 markerPosOffset;
    float yGraceTimer = 0;
    float xGraceTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainScript = GetComponent<PlayerAimMain>();
        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        movementInputs.Add(movementScript.upInput);
        movementInputs.Add(movementScript.downInput);
        movementInputs.Add(movementScript.leftInput);
        movementInputs.Add(movementScript.rightInput);
        markerPosOffset.x = 0;
        markerPosOffset.y = -offsetOnMove;
    }
    // Update is called once per frame
    void Update()
    {
        if (xGraceTimer > 0)
        {
            xGraceTimer -= Time.deltaTime;
        }
        if (yGraceTimer > 0)
        {
            yGraceTimer -= Time.deltaTime;
        }
        if (markerMoveOnInputs && markerCanMove)
        {
            HandleInputs();
        }
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
    public void SetCanMoveMarker(bool canMove)
    {
        markerCanMove = canMove;
    }
}
