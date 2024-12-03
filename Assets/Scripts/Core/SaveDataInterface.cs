using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataInterface : MonoBehaviour
{
    SaveData saveData;
    public bool loadingIngredientAmounts = false;
    public bool loadingIngredientsUnlocked = false;
    public bool loadingPotionsDiscovered = false;
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
            if (loadingIngredientAmounts)
            {
                LoadIngredientAmounts();
                loadingIngredientAmounts = false;
            }
            if (loadingIngredientsUnlocked)
            {
                LoadIngredientsUnlocked();
                loadingIngredientsUnlocked = false;
            }
            if (loadingPotionsDiscovered)
            {
                LoadPotionsDiscovered();
                loadingPotionsDiscovered = false;
            }
        }
    }
    public void SaveIngredientAmounts() { saveData.SaveIngredientAmountsToJson(); }
    public void LoadIngredientAmounts() { saveData.LoadIngredientAmountsFromJson(); }

    public void SaveIngredientsUnlocked() { saveData.SaveIngredientsUnlockedToJson(); }
    public void LoadIngredientsUnlocked() { saveData.LoadIngredientsUnlockedFromJson(); }

    public void SavePotionsDiscovered() { saveData.SavePotionsDiscoveredToJson(); }
    public void LoadPotionsDiscovered() { saveData.LoadPotionsDiscoveredFromJson(); }

    public void ResetData() { saveData.ResetIngredientAmountsData(); saveData.ResetPotionData(); }
    public void SaveAll() { SaveIngredientAmounts(); SaveIngredientsUnlocked(); SavePotionsDiscovered(); }
}
