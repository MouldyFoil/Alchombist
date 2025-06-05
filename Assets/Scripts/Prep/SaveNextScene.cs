using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNextScene : MonoBehaviour
{
    public string sceneName;
    List<string> otherVars = new List<string>();
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(FindObjectsOfType<SaveNextScene>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
    public void AddSavedVar(string name)
    {
        foreach(string var in otherVars)
        {
            if(var == name)
            {
                return;
            }
        }
        otherVars.Add(name);
    }
    public void RemoveSavedVar(string name)
    {
        otherVars.Remove(name);
    }
}
