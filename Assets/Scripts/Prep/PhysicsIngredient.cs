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
    [SerializeField] float defaultVolume;
    [SerializeField] Color ingredientColor;
    [SerializeField] AudioClip[] dropSounds;
    public int ingredientIndex;
    SFXManager soundManager;
    bool beingDragged = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundManager = FindObjectOfType<SFXManager>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        soundManager.PlayRandomAudioClip(dropSounds, transform, defaultVolume * rb.velocity.magnitude);
    }

    private void DraggedBehavior()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 propertransform = transform.position;
        rb.velocity += (mousePos - propertransform) * dragSpeed * Time.deltaTime;
    }

    private void OnMouseDown()
    {
        beingDragged = true;
    }
    public string ReturnBuffName() { return statBuffedName; }
    public float ReturnBuffAmount() { return buffAmount; }
    public Color ReturnColor() { return ingredientColor; }
}
