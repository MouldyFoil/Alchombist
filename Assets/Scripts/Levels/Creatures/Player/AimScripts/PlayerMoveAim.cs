using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAim : MonoBehaviour
{
    [SerializeField] float cornerGraceTime = 0.01f;
    [SerializeField] float offsetOnMove = 1;
    PlayerAimMain mainScript;
    [SerializeField] List<string> movementInputs;
    Vector2 markerPosWithOffset;
    float yGraceTimer = 0;
    float xGraceTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        mainScript = GetComponent<PlayerAimMain>();
        markerPosWithOffset.x = offsetOnMove;
        movementInputs.Add(movementScript.upInput);
        movementInputs.Add(movementScript.downInput);
        movementInputs.Add(movementScript.leftInput);
        movementInputs.Add(movementScript.rightInput);
    }
    private void OnEnable()
    {
        mainScript.SetMarkerVisibility(true);
        markerPosWithOffset.x = transform.position.x + offsetOnMove;
        markerPosWithOffset.y = transform.position.y + offsetOnMove;
        mainScript.marker.position = new Vector3(markerPosWithOffset.x, markerPosWithOffset.y, transform.position.z);
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
            if(xGraceTimer <= 0)
            {
                markerPosWithOffset.x = transform.position.x;
            }
            yGraceTimer = cornerGraceTime;
            markerPosWithOffset.y = transform.position.y + offsetOnMove;
            mainScript.marker.position = new Vector3(markerPosWithOffset.x, markerPosWithOffset.y, transform.position.z);
        }
        if (Input.GetKey(movementInputs[1]))
        {
            if (xGraceTimer <= 0)
            {
                markerPosWithOffset.x = transform.position.x;
            }
            yGraceTimer = cornerGraceTime;
            markerPosWithOffset.y = transform.position.y - offsetOnMove;
            mainScript.marker.position = new Vector3(markerPosWithOffset.x, markerPosWithOffset.y, transform.position.z);
        }
        if (Input.GetKey(movementInputs[2]))
        {
            if (yGraceTimer <= 0)
            {
                markerPosWithOffset.y = transform.position.y;
            }
            xGraceTimer = cornerGraceTime;
            markerPosWithOffset.x = transform.position.x - offsetOnMove;
            mainScript.marker.position = new Vector3(markerPosWithOffset.x, markerPosWithOffset.y, transform.position.z);
        }
        if (Input.GetKey(movementInputs[3]))
        {
            if (yGraceTimer <= 0)
            {
                markerPosWithOffset.y = transform.position.y;
            }
            xGraceTimer = cornerGraceTime;
            markerPosWithOffset.x = transform.position.x + offsetOnMove;
            mainScript.marker.position = new Vector3(markerPosWithOffset.x, markerPosWithOffset.y, transform.position.z);
        }
    }
}
