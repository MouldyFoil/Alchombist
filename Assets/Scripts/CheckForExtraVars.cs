using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckForExtraVars : MonoBehaviour
{
    [SerializeField] UnityEvent OnVarExists;
    [SerializeField] string varName;
    SaveNextScene saver;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        saver = FindObjectOfType<SaveNextScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if(saver != null && !activated)
        {
            foreach(string var in saver.otherVars)
            {
                if(var == varName)
                {
                    OnVarExists.Invoke();
                    activated = true;
                }
            }
        }
        else if(saver == null)
        {
            saver = FindObjectOfType<SaveNextScene>();
        }
    }
}
