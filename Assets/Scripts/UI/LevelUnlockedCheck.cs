using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockedCheck : MonoBehaviour
{
    [SerializeField] string sceneName;
    SaveDataInterface saveData;
    //Disabling buttons isnt the best for readability so this script will likely change (IF FUTURE ME SEES THIS, PLEASE CHANGE THIS SCRIPT)
    void Awake()
    {
        saveData = FindObjectOfType<SaveDataInterface>();
        bool levelUnlocked = false;
        foreach(LevelSaveData data in saveData.ReturnLevelData())
        {
            if(data.name == sceneName)
            {
                levelUnlocked = true;
                break;
            }
        }
        if (levelUnlocked)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
}
