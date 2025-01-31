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
    SpriteRenderer spriteRenderer;
    Sprite closedSprite;
    Collider2D collider;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedSprite = spriteRenderer.sprite;
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        
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
