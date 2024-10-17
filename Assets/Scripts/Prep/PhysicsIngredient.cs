using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PhysicsIngredient : MonoBehaviour
{
    [SerializeField] float dragSpeed;

    [SerializeField] string statBuffedName = "Speed";
    [SerializeField] float buffAmount = 5;

    [SerializeField] float liquidColorInfluence = 50;
    [SerializeField] Color ingredientColor;
    bool beingDragged = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        beingDragged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            beingDragged = false;
        }
        if(beingDragged == true)
        {
            DraggedBehavior();
        }
    }

    private void DraggedBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 propertransform = transform.position;
        rb.velocity = (mousePos - propertransform) * dragSpeed;
    }

    private void OnMouseDown()
    {
        beingDragged = true;
    }
    public string ReturnBuffName() { return statBuffedName; }
    public float ReturnBuffAmount() { return buffAmount; }
    public Color ReturnColor() { return ingredientColor; }
    public float ReturnColorInfluence() { return liquidColorInfluence; }
}
