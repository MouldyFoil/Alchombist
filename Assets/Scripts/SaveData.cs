using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] List<int> defaultIngredientAmounts;
    public List<int> ingredientAmounts;
    public List<bool> ingredientsUnlocked;
    public List<bool> potionsDiscovered;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType<SaveData>().Length > 1)
        {
            FindObjectOfType<SaveData>().PopulateEmptyBoolLists();
            Destroy(gameObject);
        }
        LoadIngredientAmountsFromJson();
        PopulateEmptyBoolLists();
    }
    public void PopulateEmptyBoolLists()
    {
        if(FindObjectOfType<IngredientRepository>())
        {
            PopulateIngredientsUnlocked();
        }
        if (FindObjectOfType<PotionRepository>())
        {
            PopulatePotionsDiscovered();
        }
    }
    private void PopulateIngredientsUnlocked()
    {
        while(ingredientsUnlocked.Count < FindObjectOfType<IngredientRepository>().ReturnIngredients().Length)
        {
            if (ingredientsUnlocked.Count > 1)
            {
                ingredientsUnlocked.Add(false);
            }
            else
            {
                ingredientsUnlocked.Add(true);
            }
        }
    }
    private void PopulatePotionsDiscovered()
    {
        while (potionsDiscovered.Count < FindObjectOfType<PotionRepository>().ReturnPotions().Length)
        {
            if(potionsDiscovered.Count > 0)
            {
                potionsDiscovered.Add(false);
            }
            else
            {
                potionsDiscovered.Add(true);
            }
        }
    }
    public void SaveIngredientAmountsToJson()
    {
        if (FindObjectOfType<IngredientSpawner>())
        {
            ingredientAmounts = FindObjectOfType<IngredientSpawner>().ReturnIngredientAmounts();
        }
        string prepIngredientData = JsonUtility.ToJson(ingredientAmounts);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log(System.IO.File.ReadAllText(filePath));
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

        ingredientAmounts = JsonUtility.FromJson<List<int>>(prepIngredientData);
        if(ingredientAmounts.Count <= 0)
        {
            ingredientAmounts = defaultIngredientAmounts;
        }
        Debug.Log("LOADED!");
    }
    public void ResetIngredientAmountsData()
    {
        string prepIngredientData = JsonUtility.ToJson(defaultIngredientAmounts);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        ingredientAmounts = defaultIngredientAmounts;
        Debug.Log("reset");
    }
}
