using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed;
    [SerializeField] float angularSpeedMultiplier;
    public string upInput;
    public string downInput;
    public string rightInput;
    public string leftInput;
    BuffDisplay buffDisplay;
    float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody2D>();
        buffDisplay = FindObjectOfType<BuffDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKey(upInput) && Input.GetKey(downInput) || Input.GetKey(rightInput) && Input.GetKey(leftInput))
        {
            return;
        }
        if (Input.GetKey(upInput) ^ Input.GetKey(downInput) ^ Input.GetKey(rightInput) ^ Input.GetKey(leftInput))
        {
            if (Input.GetKey(upInput))
            {
                rb.velocity = new Vector2(0, speed);
            }
            if (Input.GetKey(downInput))
            {
                rb.velocity = new Vector2(0, -speed);
            }
            if (Input.GetKey(rightInput))
            {
                rb.velocity = new Vector2(speed, 0);
            }
            if (Input.GetKey(leftInput))
            {
                rb.velocity = new Vector2(-speed, 0);
            }
        }
        else
        {
            if (Input.GetKey(upInput) && Input.GetKey(rightInput))
            {
                rb.velocity = new Vector2(speed * angularSpeedMultiplier, speed * angularSpeedMultiplier);
            }
            if (Input.GetKey(upInput) && Input.GetKey(leftInput))
            {
                rb.velocity = new Vector2(-speed * angularSpeedMultiplier, speed * angularSpeedMultiplier);
            }


            if (Input.GetKey(downInput) && Input.GetKey(rightInput))
            {
                rb.velocity = new Vector2(speed * angularSpeedMultiplier, -speed * angularSpeedMultiplier);
            }
            if (Input.GetKey(downInput) && Input.GetKey(leftInput))
            {
                rb.velocity = new Vector2(-speed * angularSpeedMultiplier, -speed * angularSpeedMultiplier);
            }
        }
    }
    public void AddMovementSpeed(float speedAddition)
    {
        speed += speedAddition;
        buffDisplay.UpdateSpeedUI(speed - baseSpeed);
    }
}
