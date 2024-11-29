using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class IngredientRepository : MonoBehaviour
{
    [SerializeField] Ingredient[] ingredients;
    SaveData saveData;
    bool alreadyLoaded = false;
    private void Update()
    {
        if(alreadyLoaded == false)
        {
            foreach (SaveData saveData1 in FindObjectsOfType<SaveData>())
            {
                if (saveData1.ingredientsUnlocked.Count == ingredients.Length)
                {
                    saveData = saveData1;
                    LoadSaveData();
                    alreadyLoaded = true;
                }
            }
        }
    }
    public void LoadSaveData()
    {
        if (saveData != null && saveData.ingredientsUnlocked.Count > 0)
        {
            int index = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.unlocked = saveData.ingredientsUnlocked[index];
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
}
[Serializable]
public class Ingredient
{
    public Sprite ingredientSprite;
    public string input;
    public int ID = Mathf.Clamp(1, 0, 10);
    public bool unlocked;
}
