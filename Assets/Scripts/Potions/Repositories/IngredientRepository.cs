using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class IngredientRepository : MonoBehaviour
{
    [SerializeField] Ingredient[] ingredients;
    SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
        saveData.PopulateEmptyBoolLists();
        LoadSaveData();
    }
    private void LoadSaveData()
    {
        List<bool> unlockedSaves = saveData.ingredientsUnlocked;
        int index = 0;
        foreach (Ingredient ingredient in ingredients)
        {
            ingredient.unlocked = unlockedSaves[index];
            index++;
        }
    }
    public void SaveIngredientsUnlocked()
    {
        List<bool> ingredientsUnlockedData = null;
        foreach(Ingredient ingredient in ingredients)
        {
            ingredientsUnlockedData.Add(ingredient.unlocked);
        }
        saveData.SaveIngredientsUnlockedToJson(ingredientsUnlockedData);
    }
    public Ingredient ReturnIngredient(int index) { return ingredients[index]; }
    public Ingredient[] ReturnIngredients() { return ingredients; }
    public void UnlockIngredient(int ingredientIndex)
    {
        ingredients[ingredientIndex].unlocked = true;
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
