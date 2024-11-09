using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionRepository : MonoBehaviour
{
    [SerializeField] Potion[] potions;
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
        List<bool> discoveredSaves = saveData.potionsDiscovered;
        int index = 0;
        foreach (Potion potion in potions)
        {
            potion.discovered = discoveredSaves[index];
            index++;
        }
    }
    public void SavePotionsDiscovered()
    {
        List<bool> potionDiscoveredData = null;
        foreach (Potion potion in potions)
        {
            potionDiscoveredData.Add(potion.discovered);
        }
        saveData.SavePotionsDiscoveredToJson(potionDiscoveredData);
    }
    public Potion ReturnPotion(int index)
    {
        return potions[index];
    }
    public int ReturnPotionsArrayLength()
    {
        return potions.Length;
    }
    public Potion[] ReturnPotions()
    {
        return potions;
    }
    public int ReturnPotionsDiscovered()
    {
        int potionsDiscovered = 0;
        foreach (Potion potion in ReturnPotions())
        {
            if(potion.discovered == true)
            {
                potionsDiscovered++;
            }
        }
        return potionsDiscovered;
    }
    public void DiscoverPotion(int potionIndex)
    {
        potions[potionIndex].discovered = true;
    }
    public void DiscoverPotionByName(string name)
    {
        foreach(Potion potion in potions)
        {
            if(potion.name == name)
            {
                potion.discovered = true;
            }
        }
    }
    
    private void OnDestroy()
    {
        
    }
}


[Serializable]
public class Potion
{
public enum potionTypeEnum { spawn_on_player = 0, spawn_on_aim = 1, spawn_as_child = 2}


    //Name of potion in recipe book
    public string name;
    //Descriptions of Potions
    public string description;
    //The gameobject/potion an input sequence instantiates
    public GameObject prefab;
    //The sprite used in the recipe book
    public Sprite spriteInBook;
    //The inputs required for a specific potion in index format
    public int ingredientCombo;
    //Type 0 = spawn on player, type 1 = spawn on aim, type 2 = spawn as child
    //public int potionType;
    public potionTypeEnum spawnType;
    //Determines if potion appears in book
    public bool discovered;
}