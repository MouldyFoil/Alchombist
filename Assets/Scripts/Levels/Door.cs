using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int buttonsRequired = 1;
    [SerializeField] bool exactChange;
    [SerializeField] Sprite openedSprite;
    [SerializeField] float boundingBoxExtention = 2;
    SpriteRenderer spriteRenderer;
    Sprite closedSprite;
    List<PressurePlate> buttons = new List<PressurePlate>();
    Collider2D collider;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedSprite = spriteRenderer.sprite;
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        CheckButtons();
    }
    public void JoinTheClub(PressurePlate button)
    {
        buttons.Add(button);
    }
    public void CheckButtons()
    {
        int buttonsPressed = 0;
        foreach(PressurePlate button in buttons)
        {
            if(button.pressed == true)
            {
                buttonsPressed++;
            }
            if(GetComponent<Collider2D>().enabled == true)
            {
                if (exactChange == false && buttonsPressed >= buttonsRequired)
                {
                    OpenDoor();
                    break;
                }
                if (exactChange == true && buttonsPressed == buttonsRequired)
                {
                    OpenDoor();
                    break;
                }
            }
        }
        if(buttonsPressed < buttonsRequired && GetComponent<Collider2D>().enabled == false)
        {
            CloseDoor();
        }
    }
    public void OpenDoor()
    {
        spriteRenderer.sprite = openedSprite;
        GetComponent<Collider2D>().enabled = false;
        UpdatePathFinding();
    }
    public void CloseDoor()
    {
        spriteRenderer.sprite = closedSprite;
        GetComponent<Collider2D>().enabled = true;
        UpdatePathFinding();
    }
    public void UpdatePathFinding()
    {
        AstarPath.active.UpdateGraphs
        (new Bounds(transform.position,
        new Vector3(collider.bounds.extents.x + boundingBoxExtention, collider.bounds.extents.y + boundingBoxExtention, collider.bounds.extents.z + boundingBoxExtention)));
    }
}
