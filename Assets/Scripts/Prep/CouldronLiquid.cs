using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CouldronLiquid : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PhysicsIngredient>() != null)
        {
            Color color = collision.GetComponent<PhysicsIngredient>().ReturnColor();
            float influence = collision.GetComponent<PhysicsIngredient>().ReturnColorInfluence();
            MoveColor(color, influence);
        }
    }
    private void MoveColor(Color color, float influence)
    {
        float hueHome, satHome, valHome;
        float hueAdd, satAdd, valAdd;
        Color.RGBToHSV(sprite.color, out hueHome, out satHome, out valHome);
        Color.RGBToHSV(color, out hueAdd, out satAdd, out valAdd);
        Debug.Log("current colors: " + "hue: " + hueHome + "sat: " + satHome + "val: " + valHome);
        Debug.Log("Added colors: " + "hue: " + hueAdd + "sat: " + satAdd + "val: " + valAdd);
        sprite.color += (Color.HSVToRGB(hueAdd, satHome, valHome) * influence);
    }
}
