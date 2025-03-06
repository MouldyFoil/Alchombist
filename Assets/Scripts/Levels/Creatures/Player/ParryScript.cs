using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    public Color[] parryTypes;
    [SerializeField] float parryTime = 0.2f;
    SpriteRenderer sprite;
    int currentParryType;
    float parryClock = 0;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (parryClock > 0)
        {
            parryClock -= Time.deltaTime;
        }
        sprite.enabled = parryClock > 0;
    }
    public void ActivateParry(int parryType)
    {
        currentParryType = parryType - 1;
        parryClock = parryTime;
        Color parryColorFinal = parryTypes[currentParryType];
        sprite.color = parryColorFinal;
    }
    public bool ReturnParryActive() { return sprite.enabled; }
    public int ReturnParryType() { return currentParryType; }
}
