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
        if (FindObjectOfType<PotionRepository>() && unlockedData.potionsDiscovered.Count > 0)
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
    }

    public void CommitUnlocksToJSON()
    {
        string unlockedDataJSON = JsonUtility.ToJson(unlockedData);
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        System.IO.File.WriteAllText(filePath, unlockedDataJSON);
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
    public void UnlockNextLevel(string nextSceneName)
    {
        bool sceneInList = false;
        int index = 0;
        foreach (LevelSaveInfo currentData in unlockedData.levelData)
        {
            if (currentData.name == nextSceneName)
            {
                sceneInList = true;
                break;
            }
            index++;
        }
        if (!sceneInList)
        {
            LevelSaveInfo newData = new LevelSaveInfo();
            newData.name = nextSceneName;
            newData.furthestCheckpoint = 0;
            unlockedData.levelData.Add(newData);
        }
    }
    public void UnlockCurrentLevel()
    {
        LevelSaveInfo newData = FindCurrentLevelData();
        bool sceneInList = false;
        int index = 0;
        foreach(LevelSaveInfo currentData in unlockedData.levelData)
        {
            if(currentData.name == newData.name)
            {
                sceneInList = true;
                break;
            }
            index++;
        }
        if (sceneInList)
        {
            //0 is a placeholder until i make the checkpoints save
            unlockedData.levelData[index].furthestCheckpoint = 0;
        }
        else
        {
            unlockedData.levelData.Add(newData);
        }
        CommitUnlocksToJSON();
    }
    private LevelSaveInfo FindCurrentLevelData()
    {
        LevelSaveInfo levelData = new LevelSaveInfo();
        levelData.name = FindObjectOfType<SceneManagement>().ReturnCurrentSceneName();
        levelData.furthestCheckpoint = 0;
        return levelData;
    }
    public List<LevelSaveInfo> ReturnLevelData()
    {
        return unlockedData.levelData;
    }
    public UnlockData ReturnUnlockData()
    {
        return unlockedData;
    }
    public void ResetUnlockedData()
    {
        unlockedData = new UnlockData();
        string unlockedDataJSON = JsonUtility.ToJson(unlockedData);
        string filePath = Application.persistentDataPath + "/UnlockedData.json";
        System.IO.File.WriteAllText(filePath, unlockedDataJSON);
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
    public List<LevelSaveInfo> levelData;
    public bool madeItPastIntro = false;
}
[System.Serializable]
public class LevelSaveInfo
{
    public string name;
    public int furthestCheckpoint;
}
