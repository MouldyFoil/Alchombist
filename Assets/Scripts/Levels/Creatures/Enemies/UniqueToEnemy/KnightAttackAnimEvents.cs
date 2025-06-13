using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnightAttackAnimEvents : MonoBehaviour
{
    [SerializeField] UnityEvent[] publicEvents;
    [SerializeField] Color[] parryColors;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateEvent(int eventIndex)
    {
        publicEvents[eventIndex].Invoke();
        Debug.Log("invoked event" + eventIndex);
    }
    public void SetParryColor(int parryIndex)
    {
        sprite.color = parryColors[parryIndex];
    }
    public void StartAnimation()
    {
        animation.Play();
    }
}
