using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CouldronLiquid : MonoBehaviour
{
    [SerializeField] AudioClip ingredientAddedSound;
    [SerializeField] float volume;
    int numberOfIngredients = 0;
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
            saveDataInterface.AddOrRemoveIngredientAmount(collision.GetComponent<PhysicsIngredient>().ingredientIndex, -1);
            soundManager.PlayAudioClip(ingredientAddedSound, transform, volume);
            Destroy(collision.gameObject);
            Color color = collision.GetComponent<PhysicsIngredient>().ReturnColor();
            MoveColor(color);
        }
    }
    //private void MoveColor(Color color)
    //{
    //    Color colorFinal = (color + sprite.color) / 2;
    //    sprite.color = colorFinal;
    //}
    private void MoveColor(Color color)
    {
        Color colorFinal = (color + (sprite.color * numberOfIngredients)) / (numberOfIngredients + 1);
        sprite.color = colorFinal;
        numberOfIngredients++;
    }
}
