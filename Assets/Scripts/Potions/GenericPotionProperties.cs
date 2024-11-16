using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class GenericPotionProperties : MonoBehaviour
{
    //this script handles duration and all the ways potions can delete or cancel eachother out (it also contains a potion name which could be used for other scripts)
    public enum cancelTypeEnum { noAmountRestriction = 0, cancelledByOthers = 1, replacesOthers = 2, maxAmountDestroysOldest = 3, maxAmountDestroysNewest = 4 }
    [SerializeField] cancelTypeEnum restrictionsOnStart;
    [SerializeField] bool noDuration;
    [SerializeField] float durationOrCooldown;
    [SerializeField] string potionName = "unnamed";
    [SerializeField] List<string> conflictingPotionNames;
    [SerializeField] int maxAmount;
    [SerializeField] TextMeshPro timerText;
    [SerializeField] AudioClip[] possibleStartSounds;
    [SerializeField] float startSoundVolume = 1;
    SFXManager soundManager;
    int potionNumber = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if(restrictionsOnStart == cancelTypeEnum.cancelledByOthers)
        {
            DestroySelfIfPotionConflict();
        }
    }
    void Start()
    {
        if (restrictionsOnStart == cancelTypeEnum.replacesOthers)
        {
            Debug.Log("AAAA");
            if (conflictingPotionNames.Count > 0)
            {
                Debug.Log("AAAA");
                CancelPotions();
            }
        }
        else if(restrictionsOnStart == cancelTypeEnum.maxAmountDestroysOldest)
        {
            DestroyOldestPotionIfMaxReached();
        }
        else if (restrictionsOnStart == cancelTypeEnum.maxAmountDestroysNewest)
        {
            DestroySelfIfMaxReached();
        }
        soundManager = FindObjectOfType<SFXManager>();
        if (possibleStartSounds != null)
        {
            soundManager.PlayRandomAudioClip(possibleStartSounds, transform, startSoundVolume);
        }
    }

    private void CancelPotions()
    {
        foreach (GenericPotionProperties potion in FindObjectsOfType<GenericPotionProperties>())
        {
            if (potion == this)
            {
                continue;
            }
            foreach (string name in conflictingPotionNames)
            {
                if (potion.ReturnPotionName() == name)
                {
                    Destroy(potion.gameObject);
                }
            }
        }
    }
    private void DestroySelfIfPotionConflict()
    {
        foreach (GenericPotionProperties potion in FindObjectsOfType<GenericPotionProperties>())
        {
            if (potion == this)
            {
                continue;
            }
            foreach (string name in conflictingPotionNames)
            {
                if (potion.ReturnPotionName() == name)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void DestroyOldestPotionIfMaxReached()
    {
            GenericPotionProperties[] potions = FindObjectsOfType<GenericPotionProperties>();
            foreach (GenericPotionProperties potion in potions)
            {
                if (potion.ReturnPotionName() == potionName)
                {
                    potion.IncreasePotionNum();
                }
                if (potion.ReturnPotionNumber() > maxAmount)
                {
                    Destroy(potion.gameObject);
                }
            }
    }
    private void DestroySelfIfMaxReached()
    {
        GenericPotionProperties[] potions = FindObjectsOfType<GenericPotionProperties>();
        foreach (GenericPotionProperties potion in potions)
        {
            foreach (string name in conflictingPotionNames)
            {
                if (potion.ReturnPotionName() == name)
                {
                    potionNumber++;
                }
            }
            if (potionNumber > maxAmount)
            {
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {
        if(noDuration == false)
        {
            durationOrCooldown -= Time.deltaTime;
            if(timerText != null)
            {
                UpdateTimerText();
            }
            if (durationOrCooldown < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateTimerText()
    {
        float displayTime = durationOrCooldown * 10;
        displayTime = (int)displayTime;
        displayTime = displayTime / 10;
        timerText.text = displayTime.ToString();
    }

    public string ReturnPotionName() { return potionName; }
    public void IncreasePotionNum()
    {
        potionNumber++;
    }
    public void DecreasePotionNum()
    {
        potionNumber--;
    }
    public int ReturnPotionNumber() { return potionNumber; }
}
