using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPotion : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] int maxAmount;
    [SerializeField] bool infiniteDuration;
    [SerializeField] bool infiniteAmount;
    [SerializeField] string objectName;
    int potionNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(infiniteAmount == false)
        {
            DestroyOldestPotionIfMaxReached();
        }
    }
    void Update()
    {
        if(infiniteDuration == false)
        {
            duration -= Time.deltaTime;
            if(duration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void DestroyOldestPotionIfMaxReached()
    {
        if (infiniteAmount == false)
        {
            ObjectPotion[] objectPotions = GameObject.FindObjectsOfType<ObjectPotion>();
            foreach (ObjectPotion objectPotion in objectPotions)
            {
                if (objectPotion.ReturnObjectName() == objectName)
                {
                    objectPotion.IncreasePotionNum();
                }
                if (objectPotion.ReturnPotionNumber() > maxAmount)
                {
                    Destroy(objectPotion.gameObject);
                }
            }
        }
    }
    public void IncreasePotionNum()
    {
        potionNumber++;
    }
    public void DecreasePotionNum()
    {
        potionNumber--;
    }
    public string ReturnObjectName() { return objectName; }
    public int ReturnPotionNumber() { return potionNumber; }
}
