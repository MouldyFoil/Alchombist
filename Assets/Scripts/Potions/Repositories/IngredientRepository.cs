using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class IngredientRepository : MonoBehaviour
{
    [SerializeField] Ingredient[] ingredients;
    SaveData saveData;
    public void LoadSaveData(SaveData saveDataIn)
    {
        saveData = saveDataIn;
        if (saveData != null && saveData.ingredientsUnlocked.Count > 0)
        {
            List<bool> unlockedSaves = saveData.ingredientsUnlocked;
            int index = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                Debug.Log(unlockedSaves[index]);
                ingredient.unlocked = unlockedSaves[index];
                index++;
            }
        }
    }
    public void SaveIngredientsUnlocked()
    {
        List<bool> ingredientsUnlockedData = new List<bool>();
        foreach(Ingredient ingredient in ingredients)
        {
            ingredientsUnlockedData.Add(ingredient.unlocked);
        }
        if(saveData != null)
        {
            saveData.SaveIngredientsUnlockedToJson(ingredientsUnlockedData);
        }
        else
        {
            Debug.Log("SaveData is NULL!");
        }
    }
    public Ingredient ReturnIngredient(int index) { return ingredients[index]; }
    public Ingredient[] ReturnIngredients() { return ingredients; }
    public void UnlockIngredient(int ingredientIndex)
    {
        ingredients[ingredientIndex].unlocked = true;
        SaveIngredientsUnlocked();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class Ingredient
{
    public Sprite ingredientSprite;
    public string input;
    public int ID = Mathf.Clamp(1, 0, 10);
    public bool unlocked;
}
