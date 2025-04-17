using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionRepository : MonoBehaviour
{
    [SerializeField] Potion[] potions;
    SaveDataInterface saveDataInterface;
    private void Start()
    {
        saveDataInterface = FindObjectOfType<SaveDataInterface>();
        saveDataInterface.settingPotionsDiscovered = true;
    }
    private void Update()
    {
        if (potions[0].discovered == false)
        {
            potions[0].discovered = true;
        }
    }
    public void SetDiscoveredStatuses(List<bool> discoveredList)
    {
        int index = 0;
        foreach (Potion potion in potions)
        {
            if(index > discoveredList.Count - 1)
            {
                return;
            }
            potion.discovered = discoveredList[index];
            index++;
        }
    }
    public Potion ReturnPotion(int index) { return potions[index]; }
    public int ReturnPotionsArrayLength()
    { return potions.Length; }
    public Potion[] ReturnPotions() { return potions; }
    public int ReturnPotionsDiscoveredNum()
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
    public List<bool> ReturnPotionsDiscoveredList()
    {
        List<bool> potionsDiscovered = new List<bool>();
        foreach(Potion potion in ReturnPotions())
        {
            potionsDiscovered.Add(potion.discovered);
        }
        return potionsDiscovered;
    }
    public void DiscoverPotionByName(string name)
    {
        foreach(Potion potion in potions)
        {
            if(potion.name == name)
            {
                potion.discovered = true;
                break;
            }
        }
        saveDataInterface.SavePotionData();
    }
    
    private void OnDestroy()
    {
        
    }
}


[Serializable]
public class Potion
{
public enum potionTypeEnum { spawn_on_player = 0, spawn_on_aim = 1, spawn_as_child = 2, spawn_on_crosshair = 3}


    //Name of potion in recipe book
    public string name;
    //Descriptions of Potions
    [TextArea]
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
    //only set true for potions hidden in puzzles (like a potion code hidden in the OST or something)
    public bool discoverOnCreate;
}