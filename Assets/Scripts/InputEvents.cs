using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour
{
    [SerializeField] bool timeBound;
    [SerializeField] float timer = 5;
    [SerializeField] string inputString;
    [SerializeField] int amountRequired = 1;
    [SerializeField] float delay = 5;
    [SerializeField] UnityEvent eventOnRequirementsMet;
    [SerializeField] UnityEvent lateEvent;
    bool startedRequirement = false;
    int currentAmount;
    bool completed = false;
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
        if (Input.GetKeyDown(inputString))
        {
            currentAmount++;
        }
        if (currentAmount >= amountRequired && completed == false)
        {
            completed = true;
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
            gameObject.SetActive(false);
        }
    }
}
