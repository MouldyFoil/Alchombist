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
    PrepIngredientData prepData = new PrepIngredientData();

    UnlockData unlockedData = new UnlockData();
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
        DontDestroyOnLoad(gameObject);
        CreateFileDirectories();
        LoadAll();
    }
    private void CreateFileDirectories()
    {
        CreateUnlockedDirectory();
        CreatePrepDirectory();
    }
    private void CreateUnlockedDirectory()
    {
        
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        if (!System.IO.File.Exists(filePath))
        {
            ResetUnlockedData();
        }
    }
    private void CreatePrepDirectory()
    {
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        if (!System.IO.File.Exists(filePath))
        {
            ResetIngredientAmountsData();
        }
    }
    public void LoadAll()
    {
        LoadIngredientAmountsFromJson();
        LoadUnlockedData();
    }
    //populate failsafes
    private void PopulateIngredientsUnlocked()
    {
        if (FindObjectOfType<IngredientRepository>())
        {
            while (unlockedData.ingredientsUnlocked.Count < FindObjectOfType<IngredientRepository>().ReturnIngredients().Length)
            {
                unlockedData.ingredientsUnlocked.Add(false);
            }
        }
    }
    private void PopulatePotionsDiscovered()
    {
        if (FindObjectOfType<PotionRepository>())
        {
            while (unlockedData.potionsDiscovered.Count < FindObjectOfType<PotionRepository>().ReturnPotions().Length)
            {
                unlockedData.potionsDiscovered.Add(false);
            }
        }
    }
    // active potion things below
    public void SetIngredientsUnlocked()
    {
        if (FindObjectOfType<IngredientRepository>() && unlockedData.ingredientsUnlocked != null && unlockedData.ingredientsUnlocked.Count > 0)
        {
            if(unlockedData.ingredientsUnlocked != null)
            {
                FindObjectOfType<IngredientRepository>().SetUnlockedStatuses(unlockedData.ingredientsUnlocked);
            }
            else
            {
                unlockedData.ingredientsUnlocked = new List<bool>();
                PopulateIngredientsUnlocked();
            }
        }
    }
    public void SetPotionsDiscovered()
    {
        if (unlockedData.potionsDiscovered.Count > 0 && FindObjectOfType<PotionRepository>())
        {
            if (unlockedData.potionsDiscovered != null)
            {
                FindObjectOfType<PotionRepository>().SetDiscoveredStatuses(unlockedData.potionsDiscovered);
            }
            else
            {
                unlockedData.potionsDiscovered = new List<bool>();
                PopulatePotionsDiscovered();
            }
        }
    }
    public void SavePotionData()
    {
        if (FindObjectOfType<IngredientRepository>())
        {
            unlockedData.ingredientsUnlocked = FindObjectOfType<IngredientRepository>().ReturnIngredientsUnlockedList();
        }
        if (FindObjectOfType<PotionRepository>())
        {
            unlockedData.potionsDiscovered = FindObjectOfType<PotionRepository>().ReturnPotionsDiscoveredList();
        }
        string potionDataJSON = JsonUtility.ToJson(unlockedData);
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        System.IO.File.WriteAllText(filePath, potionDataJSON);
    }
    public void LoadUnlockedData()
    {
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        string potionDataJSON = System.IO.File.ReadAllText(filePath);

        unlockedData = JsonUtility.FromJson<UnlockData>(potionDataJSON);
    }
    // prep ingredients below
    public void SaveIngredientAmountsToJson()
    {
        string prepIngredientData = JsonUtility.ToJson(prepData);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log(System.IO.File.ReadAllText(filePath));
        Debug.Log("ingredient amounts save updated");
    }
    public void SetIngredientAmounts()
    {
        if (FindObjectOfType<IngredientSpawner>())
        {
            FindObjectOfType<IngredientSpawner>().SetIngredientAmounts(prepData.ingredientAmounts);
        }
    }
    public void AddOrRemoveIngredientAmount(int index, int amount)
    {
        prepData.ingredientAmounts[index] += amount;
    }
    public void LoadIngredientAmountsFromJson()
    {
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        string prepIngredientData = System.IO.File.ReadAllText(filePath);

        prepData = JsonUtility.FromJson<PrepIngredientData>(prepIngredientData);
        if(prepData.ingredientAmounts.Count <= 0)
        {
            prepData.ingredientAmounts = ingredientAmountsFallback.ToList();
        }
        Debug.Log("LOADED INGREDIENT AMOUNTS");
    }
    public void ResetIngredientAmountsData()
    {
        prepData.ingredientAmounts = ingredientAmountsFallback.ToList();
        string prepIngredientData = JsonUtility.ToJson(prepData);
        string filePath = Application.persistentDataPath + "/PrepIngredientData.json";
        System.IO.File.WriteAllText(filePath, prepIngredientData);
        Debug.Log(filePath);
        Debug.Log("reset");
    }
    public void ResetUnlockedData()
    {
        unlockedData = new UnlockData();
        string potionDataJSON = JsonUtility.ToJson(unlockedData);
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        System.IO.File.WriteAllText(filePath, potionDataJSON);
    }
}
[System.Serializable]
public class PrepIngredientData
{
    public List<int> ingredientAmounts;
}
[System.Serializable]
public class UnlockData
{
    public List<bool> ingredientsUnlocked;
    public List<bool> potionsDiscovered;
}
