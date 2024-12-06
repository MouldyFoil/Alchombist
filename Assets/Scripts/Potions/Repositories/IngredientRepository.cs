using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class IngredientRepository : MonoBehaviour
{
    [SerializeField] Ingredient[] ingredients;
    SaveDataInterface saveDataInterface;
    private void Start()
    {
        saveDataInterface = FindObjectOfType<SaveDataInterface>();
        saveDataInterface.settingIngredientsUnlocked = true;
    }
    public void SetUnlockedStatuses(List<bool> unlockedList)
    {
        int index = 0;
        foreach(Ingredient ingredient in ingredients)
        {
            ingredient.unlocked = unlockedList[index];
            index++;
        }
    }
    public List<bool> ReturnIngredientsUnlockedList()
    {
        List<bool> IngredientsUnlocked = new List<bool>();
        foreach (Ingredient ingredient in ReturnIngredients())
        {
            IngredientsUnlocked.Add(ingredient.unlocked);
        }
        return IngredientsUnlocked;
    }
    public Ingredient ReturnIngredient(int index) { return ingredients[index]; }
    public Ingredient[] ReturnIngredients() { return ingredients; }
    public void UnlockIngredient(int ingredientIndex)
    {
        ingredients[ingredientIndex].unlocked = true;
        saveDataInterface.SavePotionData();
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
