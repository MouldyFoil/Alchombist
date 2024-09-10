using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform aimTransform;
    Rigidbody2D rb;
    Vector3 target;
    Vector3 aimDirection;
    bool aimToggle = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerMovement>() == null)
        {
            Debug.Log("no player");
        }
        else
        {
            target = FindObjectOfType<PlayerMovement>().transform.position;
            if(aimToggle == true)
            {
                AimAtTarget();
            }
        }
    }
    public void ToggleAim(bool aiming)
    {
        aimToggle = aiming;
    }
    public Vector3 ReturnTarget() { return target; }
    private void AimAtTarget()
    {
        target.z = 0;
        aimDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void ResetVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
    public void Circle(float speed)
    {
        rb.velocity = aimTransform.up * speed;
    }
    public void Dash(float dashSpeed)
    {
        Vector2 vector2Speed = aimTransform.right * dashSpeed;
        rb.velocity += vector2Speed;
    }
    public void DodgeLeftOrRight(float dodgeSpeed)
    {
        Vector2 vector2Speed = aimTransform.up * dodgeSpeed;
        rb.velocity += vector2Speed;
    }
    public void MoveTowardsOrBack(float customSpeed)
    {
        rb.velocity = aimTransform.right * customSpeed;
    }
}
