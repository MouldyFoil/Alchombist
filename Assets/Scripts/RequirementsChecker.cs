using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsChecker : MonoBehaviour
{
    [SerializeField] Requirement requirement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[Serializable]
public class Requirement
{
    public string name;
    public bool requirementStarted;
    public bool completed;
    public string nameOfThingRequired;
    public int intRequirement;
    public int currentIntValue = 0;
}
