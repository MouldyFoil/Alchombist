using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralRequirementScript : MonoBehaviour
{
    [SerializeField] bool oneTimeCheck = true;
    [SerializeField] bool requireOnStart = false;
    [SerializeField] int amountRequired = 1;
    [SerializeField] bool timeBound;
    [Header("UniversalEvent occurs when timebound is off")]
    [Header("and occurs every timed event")]
    [SerializeField] UnityEvent universalEvent;
    [SerializeField] TimeBoundEvent[] timedEvents;
    [SerializeField] UnityEvent undoEvent;
    float timer = 0;
    bool startedRequirement = false;
    [SerializeField] int currentAmount;
    bool completed;

    private void Start()
    {
        if (requireOnStart)
        {
            startedRequirement = true;
        }
    }
    void Update()
    {
        if (startedRequirement && !completed)
        {
            timer += Time.deltaTime;
        }
    }
    public void StartChecking()
    {
        startedRequirement = true;
    }
    public void TickUpRequirement()
    {
        if (startedRequirement && !completed || !oneTimeCheck)
        {
            currentAmount++;
            if (currentAmount >= amountRequired)
            {
                if (timeBound)
                {
                    HandleTimeBoundEvents();
                }
                else
                {
                    universalEvent.Invoke();
                    completed = true;
                }
            }
        }
    }
    public void TickDownRequirement()
    {
        if (!oneTimeCheck)
        {
            if (currentAmount == amountRequired)
            {
                undoEvent.Invoke();
            }
            currentAmount--;
        }
    }
    private void HandleTimeBoundEvents()
    {
        UnityEvent lastEvent = null;
        int index = 0;
        foreach (TimeBoundEvent timeEvent in timedEvents)
        {
            index++;
            if (timer >= timeEvent.timeInQuestion)
            {
                lastEvent = timeEvent.eventInQuestion;
            }
            else
            {
                if (lastEvent != null)
                {
                    lastEvent.Invoke();
                }
                else
                {
                    timeEvent.eventInQuestion.Invoke();
                }
                universalEvent.Invoke();
            }
            if(index == timedEvents.Length)
            {
                lastEvent.Invoke();
            }
        }
        completed = true;
    }
}
[Serializable]
public class TimeBoundEvent
{
    public UnityEvent eventInQuestion;
    public float timeInQuestion;
}
