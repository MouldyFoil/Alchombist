using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CouldronLiquid : MonoBehaviour
{
    [SerializeField] AudioClip ingredientAddedSound;
    [SerializeField] float volume;
    SFXManager soundManager;
    SpriteRenderer sprite;
    PrepBuffs prepBuffs;
    SaveDataInterface saveDataInterface;
    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        prepBuffs = FindObjectOfType<PrepBuffs>();
        soundManager = FindObjectOfType<SFXManager>();
        saveDataInterface = FindObjectOfType<SaveDataInterface>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PhysicsIngredient>() != null)
        {
            PhysicsIngredient ingredientScript = collision.GetComponent<PhysicsIngredient>();
            prepBuffs.IncreaseStat(ingredientScript.ReturnBuffName(), ingredientScript.ReturnBuffAmount());
            saveDataInterface.DecreaseIngredientAmount(collision.GetComponent<PhysicsIngredient>().ingredientIndex);
            soundManager.PlayAudioClip(ingredientAddedSound, transform, volume);
            Destroy(collision.gameObject);
            //Color color = collision.GetComponent<PhysicsIngredient>().ReturnColor();
            //float influence = collision.GetComponent<PhysicsIngredient>().ReturnColorInfluence();
            //MoveColor(color, influence);
        }
    }
    //private void MoveColor(Color color, float influence)
    //{
    //    float hueHome, satHome, valHome;
    //    float hueGoal, satGoal, valGoal;
    //    Color.RGBToHSV(sprite.color, out hueHome, out satHome, out valHome);
    //    Color.RGBToHSV(color, out hueGoal, out satGoal, out valGoal);
    //    Debug.Log("current colors: " + "hue: " + hueHome + "sat: " + satHome + "val: " + valHome);
    //    Debug.Log("Added colors: " + "hue: " + hueGoal);
    //    if(hueHome <= hueGoal)
    //    {
    //        sprite.color += (Color.HSVToRGB(hueGoal, 0, 0));
    //        if (hueHome > hueGoal)
    //        {
    //            sprite.color = (Color.HSVToRGB(hueGoal, satGoal, valGoal));
    //        }
    //    }
    //    if(hueHome >= hueGoal)
    //    {
    //        sprite.color -= (Color.HSVToRGB(hueGoal * influence, 0, 0));
    //        if(hueHome < hueGoal)
    //        {
    //            sprite.color = (Color.HSVToRGB(hueGoal, satGoal, valGoal));
    //        }
    //    }
    //}
}
