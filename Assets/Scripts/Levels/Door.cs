using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool exactChange;
    [SerializeField] Sprite openedSprite;
    [SerializeField] float boundingBoxExtention = 2;
    [SerializeField] bool startOpened = false;
    SpriteRenderer spriteRenderer;
    Sprite closedSprite;
    Collider2D doorCollider;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedSprite = spriteRenderer.sprite;
        doorCollider = GetComponent<Collider2D>();
        if (startOpened)
        {
            OpenDoor();
        }
    }
    private void Update()
    {
        
    }
    public void OpenDoor()
    {
        if (spriteRenderer)
        {
            spriteRenderer.sprite = openedSprite;
        }
        else
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = openedSprite;
        }
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
        new Vector3(doorCollider.bounds.extents.x + boundingBoxExtention, doorCollider.bounds.extents.y + boundingBoxExtention, doorCollider.bounds.extents.z + boundingBoxExtention)));
    }
}
