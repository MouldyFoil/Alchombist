using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class DurationAndConflictHandler : MonoBehaviour
{
    public enum cancelTypeEnum { doesntConflict = 0, conflictDestroysSelf = 1, conflictDestroysOthers = 2, maxAmountDestroysOldest = 3, maxAmountDestroysSelf = 4 }
    [SerializeField] cancelTypeEnum cancelType;
    [SerializeField] bool noDuration;
    [SerializeField] float durationOrCooldown;
    [SerializeField] string potionName;
    [SerializeField] List<string> conflictingPotionNames;
    [SerializeField] int maxAmount;
    [SerializeField] TextMeshPro timerText;
    int potionNumber = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if(cancelType == cancelTypeEnum.conflictDestroysSelf)
        {
            DestroySelfIfPotionConflict();
        }
    }
    void Start()
    {
        if (cancelType == cancelTypeEnum.conflictDestroysOthers)
        {
            if(conflictingPotionNames.Count > 0)
            {
                CancelPotions();
            }
        }
        else if(cancelType == cancelTypeEnum.maxAmountDestroysOldest)
        {
            DestroyOldestPotionIfMaxReached();
        }
        else if (cancelType == cancelTypeEnum.maxAmountDestroysSelf)
        {
            DestroySelfIfMaxReached();
        }
    }

    private void CancelPotions()
    {
        foreach (DurationAndConflictHandler potion in FindObjectsOfType<DurationAndConflictHandler>())
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
        foreach (DurationAndConflictHandler potion in FindObjectsOfType<DurationAndConflictHandler>())
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
            DurationAndConflictHandler[] potions = FindObjectsOfType<DurationAndConflictHandler>();
            foreach (DurationAndConflictHandler potion in potions)
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
        DurationAndConflictHandler[] potions = FindObjectsOfType<DurationAndConflictHandler>();
        foreach (DurationAndConflictHandler potion in potions)
        {
            if (potion.ReturnPotionName() == potionName)
            {
                potionNumber++;
            }
            if (potionNumber > maxAmount)
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(noDuration == false)
        {
            durationOrCooldown -= Time.deltaTime;
            if(timerText != null)
            {
                float displayTime = durationOrCooldown * 10;
                displayTime = (int)displayTime;
                displayTime = displayTime / 10;
                timerText.text = displayTime.ToString();
            }
            if (durationOrCooldown < 0)
            {
                Destroy(gameObject);
            }
        }
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
