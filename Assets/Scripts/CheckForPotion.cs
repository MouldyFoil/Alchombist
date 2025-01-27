using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckForPotion : MonoBehaviour
{
    [SerializeField] bool timeBound;
    [SerializeField] float timer = 5;
    [SerializeField] string potionName;
    [SerializeField] int amountRequired = 1;
    [SerializeField] UnityEvent eventOnRequirementsMet;
    [SerializeField] UnityEvent lateEvent;
    bool startedRequirement = false;
    [SerializeField] int currentAmount;
    bool completed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartChecking()
    {
        startedRequirement = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeBound && startedRequirement)
        {
            timer -= Time.deltaTime;
        }
        if (FindObjectOfType<GenericPotionProperties>())
        {
            foreach(GenericPotionProperties potion in FindObjectsOfType<GenericPotionProperties>())
            {
                if (potion.ReturnPotionName() == potionName && !potion.CheckChecked())
                {
                    potion.GetChecked();
                    currentAmount++;
                }
            }
            
        }
        if(currentAmount >= amountRequired && completed == false)
        {
            if (timeBound && timer > 0)
            {
                eventOnRequirementsMet.Invoke();
            }
            else if(timeBound && timer <= 0)
            {
                lateEvent.Invoke();
            }
            else
            {
                eventOnRequirementsMet.Invoke();
            }
            completed = true;
            gameObject.SetActive(false);
        }
    }
}
