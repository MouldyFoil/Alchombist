using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public int[] ingredientAmounts;
    public bool[] ingredientsUnlocked;
    public bool[] potionsDiscovered;
    private void Start()
    {
        LoadIngredientAmountsFromJson();
    }
    public void SaveIngredientAmountsToJsonButton()
    {
        if (FindObjectOfType<IngredientSpawner>())
        {
            ingredientAmounts = FindObjectOfType<IngredientSpawner>().ReturnIngredientAmounts();
        }
        string prepIngredientData = JsonUtility.ToJson(ingredientAmounts);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log("save updated");
    }
    public void AddToIngredients(int index, int amount)
    {
        ingredientAmounts[index] += amount;
    }
    public void LoadIngredientAmountsFromJson()
    {
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        string prepIngredientData = System.IO.File.ReadAllText(filePath);

        ingredientAmounts = JsonUtility.FromJson<int[]>(prepIngredientData);
        Debug.Log("LOADED!");
    }
    public void ResetIngredientAmountsData()
    {
        int[] emptyIntArray = null;
        string prepIngredientData = JsonUtility.ToJson(emptyIntArray);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log("reset");
    }
}
