using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManualAim : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    [SerializeField] Transform marker;
    Vector3 mousePos;
    Vector3 aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {

    }
    void Update()
    {
        if (!marker.GetComponent<SpriteRenderer>().enabled)
        {
            marker.GetComponent<SpriteRenderer>().enabled = true;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AimAtMouse();
    }
    private void AimAtMouse()
    {
        mousePos.z = transform.position.z;
        aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        marker.position = mousePos;
    }
    
}
