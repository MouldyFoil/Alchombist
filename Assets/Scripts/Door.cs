using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int buttonsRequired = 1;
    [SerializeField] bool exactChange;
    [SerializeField] Sprite openedSprite;
    SpriteRenderer spriteRenderer;
    Sprite closedSprite;
    List<PressurePlate> buttons = new List<PressurePlate>();
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedSprite = spriteRenderer.sprite;
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
        AstarPath.active.Scan();
    }
    public void CloseDoor()
    {
        spriteRenderer.sprite = closedSprite;
        GetComponent<Collider2D>().enabled = true;
        AstarPath.active.Scan();
    }
}
