using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] bool dontSaveScene = false;
    [SerializeField] int[] ingredientAmountsFallback;
    public bool saveDataPrime;
    public List<int> ingredientAmounts;

    public List<bool> ingredientsUnlocked;
    public List<bool> potionsDiscovered;
    private void Awake()
    {
        if(dontSaveScene == true)
        {
            foreach (SaveData saveData in FindObjectsOfType<SaveData>())
            {
                if(saveData != this)
                {
                    Destroy(saveData.gameObject);
                }
            }
            Destroy(gameObject);
            return;
        }
        if (FindObjectsOfType<SaveData>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        saveDataPrime = true;
        LoadAll();
        DontDestroyOnLoad(gameObject);
    }
    public void LoadAll()
    {
        LoadIngredientAmountsFromJson();
        LoadIngredientsUnlockedFromJson();
        LoadPotionsDiscoveredFromJson();
    }
    //populate failsafes
    private void PopulateIngredientsUnlocked()
    {
        if (FindObjectOfType<IngredientRepository>())
        {
            while (ingredientsUnlocked.Count < FindObjectOfType<IngredientRepository>().ReturnIngredients().Length)
            {
                ingredientsUnlocked.Add(false);
            }
        }
    }
    private void PopulatePotionsDiscovered()
    {
        if (FindObjectOfType<PotionRepository>())
        {
            while (potionsDiscovered.Count < FindObjectOfType<PotionRepository>().ReturnPotions().Length)
            {
                potionsDiscovered.Add(false);
            }
        }
    }
    // active ingredients below
    public void SaveIngredientsUnlockedToJson()
    {
        if (FindObjectOfType<IngredientRepository>())
        {
            ingredientsUnlocked = FindObjectOfType<IngredientRepository>().ReturnIngredientsUnlockedList();
        }
        string prepIngredientData = JsonUtility.ToJson(ingredientsUnlocked);
        string filePath = Application.persistentDataPath + "/ingredientsUnlockedData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log(System.IO.File.ReadAllText(filePath));
        Debug.Log("ingredients unlocked save updated");
    }
    public void LoadIngredientsUnlockedFromJson()
    {
        string filePath = Application.persistentDataPath + "/ingredientsUnlockedData.json";
        string ingredientsUnlockedData = System.IO.File.ReadAllText(filePath);

        ingredientsUnlocked = JsonUtility.FromJson<List<bool>>(ingredientsUnlockedData);
        PopulateIngredientsUnlocked();
        if (FindObjectOfType<IngredientRepository>())
        {
            FindObjectOfType<IngredientRepository>().SetUnlockedStatuses(ingredientsUnlocked);
        }
        Debug.Log("LOADED INGREDIENTS UNLOCKED");
    }
    // potions below
    public void SavePotionsDiscoveredToJson()
    {
        if (FindObjectOfType<PotionRepository>())
        {
            potionsDiscovered = FindObjectOfType<PotionRepository>().ReturnPotionsDiscoveredList();
        }
        string prepIngredientData = JsonUtility.ToJson(potionsDiscovered);
        string filePath = Application.persistentDataPath + "/potionsDiscoveredData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log(System.IO.File.ReadAllText(filePath));
        Debug.Log("potions discovered save updated");
    }
    public void LoadPotionsDiscoveredFromJson()
    {
        string filePath = Application.persistentDataPath + "/potionsDiscoveredData.json";
        string ingredientsUnlockedData = System.IO.File.ReadAllText(filePath);

        potionsDiscovered = JsonUtility.FromJson<List<bool>>(ingredientsUnlockedData);
        PopulatePotionsDiscovered();
        if (FindObjectOfType<PotionRepository>())
        {
            FindObjectOfType<PotionRepository>().SetDiscoveredStatuses(potionsDiscovered);
        }
        Debug.Log("LOADED POTIONS DISCOVERED");
    }
    // prep ingredients below
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
        Debug.Log("ingredient amounts save updated");
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
            ingredientAmounts = ingredientAmountsFallback.ToList();
        }
        if (FindObjectOfType<IngredientSpawner>())
        {
            FindObjectOfType<IngredientSpawner>().SetIngredientAmounts(ingredientAmounts);
        }
        Debug.Log("LOADED INGREDIENT AMOUNTS");
    }
    public void ResetIngredientAmountsData()
    {
        string prepIngredientData = JsonUtility.ToJson(ingredientAmountsFallback);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        ingredientAmounts = ingredientAmountsFallback.ToList();
        Debug.Log("reset");
    }
    public void ResetPotionData()
    {
        string emptyList = JsonUtility.ToJson(new List<bool>());
        string filePath = Application.persistentDataPath + "/ingredientsUnlockedData.json";
        string filePath2 = Application.persistentDataPath + "/potionsDiscoveredData.json";
        Debug.Log(filePath + " and " + filePath2);
        System.IO.File.WriteAllText(filePath, emptyList);
        System.IO.File.WriteAllText(filePath2, emptyList);
    }
}
