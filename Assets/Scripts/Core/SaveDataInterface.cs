using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataInterface : MonoBehaviour
{
    [SerializeField] bool unlockLevelOnStart = true;
    SaveData saveData;
    public bool settingIngredientAmounts = false;
    public bool settingIngredientsUnlocked = false;
    public bool settingPotionsDiscovered = false;
    public bool unlockingCurrentLevel = false;
    void Start()
    {
        if (unlockLevelOnStart)
        {
            unlockingCurrentLevel = true;
        }
        if (FindObjectsOfType<SaveData>().Length == 1)
        {
            saveData = FindObjectOfType<SaveData>();
        }
        else
        {
            foreach (SaveData save in FindObjectsOfType<SaveData>())
            {
                if (save.saveDataPrime == true)
                {
                    saveData = save;
                    return;
                }
            }
            saveData = FindObjectOfType<SaveData>();
        }
    }
    void Update()
    {
        if(saveData == null && FindObjectsOfType<SaveData>().Length == 1)
        {
            saveData = FindObjectOfType<SaveData>();
        }
        if(saveData != null && saveData.saveDataPrime)
        {
            if (settingIngredientAmounts)
            {
                SetIngredientAmounts();
                settingIngredientAmounts = false;
                FindObjectOfType<IngredientSpawner>().UpdateIngredientInfo();
            }
            if (settingIngredientsUnlocked)
            {
                SetIngredientsUnlocked();
                settingIngredientsUnlocked = false;
            }
            if (settingPotionsDiscovered)
            {
                SetPotionsDiscovered();
                settingPotionsDiscovered = false;
            }
            if (unlockingCurrentLevel)
            {
                UnlockCurentLevel();
                unlockingCurrentLevel = false;
            }
        }
    }

    public void SaveIngredientAmounts() { saveData.SaveIngredientAmountsToJson(); }
    public void SetIngredientAmounts() { saveData.SetIngredientAmounts(); }
    public void AddOrRemoveIngredientAmount(int index, int amount) { saveData.AddOrRemoveIngredientAmount(index, amount); }
    public void LoadIngredientAmounts() { saveData.LoadIngredientAmountsFromJson(); }

    public void SavePotionData() { saveData.SavePotionData(); }
    public void LoadPotionData() { saveData.LoadUnlockedData(); }
    public void SetIngredientsUnlocked() { saveData.SetIngredientsUnlocked(); }

    public void SetPotionsDiscovered() { saveData.SetPotionsDiscovered(); }

    public void UnlockCurentLevel() { saveData.UnlockCurrentLevel(); Debug.Log("AAAAAAAA"); }
    public void UnlockNextLevel(string sceneName) { saveData.UnlockNextLevel(sceneName); }
    public List<LevelSaveInfo> ReturnLevelData() { return saveData.ReturnLevelData(); }

    public void ResetData() { saveData.ResetIngredientAmountsData(); saveData.ResetUnlockedData(); }
    public void SaveAll() { SaveIngredientAmounts(); SavePotionData(); }
    public void CommitUnlocksToJSON() { saveData.CommitUnlocksToJSON(); }
    public UnlockData ReturnAllUnlockData() { return saveData.ReturnUnlockData(); }
}
