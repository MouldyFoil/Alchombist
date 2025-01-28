using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralRequirementScript : MonoBehaviour
{
    [SerializeField] bool requireOnStart;
    [SerializeField] int amountRequired = 1;
    [SerializeField] bool timeBound;
    [Header("UniversalEvent happens with every timed event and is used when not timebound")]
    [SerializeField] UnityEvent universalEvent;
    [SerializeField] TimeBoundEvent[] timedEvents;
    float timer;
    bool startedRequirement = false;
    int currentAmount;
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
        if (startedRequirement)
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
        if (startedRequirement)
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
                }
            }
        }
    }

    private void HandleTimeBoundEvents()
    {
        UnityEvent lastEvent = new UnityEvent();
        foreach (TimeBoundEvent timeEvent in timedEvents)
        {
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
        }
    }
}
[Serializable]
public class TimeBoundEvent
{
    public UnityEvent eventInQuestion;
    public float timeInQuestion;
}
