using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataInterface : MonoBehaviour
{
    SaveData saveData;
    public bool settingIngredientAmounts = false;
    public bool settingIngredientsUnlocked = false;
    public bool settingPotionsDiscovered = false;
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    public void SaveIngredientAmounts() { saveData.SaveIngredientAmountsToJson(); }
    public void SetIngredientAmounts() { saveData.SetIngredientAmounts(); }
    public void DecreaseIngredientAmount(int index) { saveData.DecreaseIngredientAmountByOne(index); }
    public void LoadIngredientAmounts() { saveData.LoadIngredientAmountsFromJson(); }

    public void SaveIngredientsUnlocked() { saveData.SaveIngredientsUnlockedToJson(); }
    public void SetIngredientsUnlocked() { saveData.SetIngredientsUnlocked(); }
    public void LoadIngredientsUnlocked() { saveData.LoadIngredientsUnlockedFromJson(); }

    public void SavePotionsDiscovered() { saveData.SavePotionsDiscoveredToJson(); }
    public void SetPotionsDiscovered() { saveData.SetPotionsDiscovered(); }
    public void LoadPotionsDiscovered() { saveData.LoadPotionsDiscoveredFromJson(); }

    public void ResetData() { saveData.ResetIngredientAmountsData(); saveData.ResetPotionData(); }
    public void SaveAll() { SaveIngredientAmounts(); SaveIngredientsUnlocked(); SavePotionsDiscovered(); }
}
