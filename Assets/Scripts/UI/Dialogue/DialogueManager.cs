using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] string nextKey = "space";
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject page in pages)
        {
            page.GetComponent<DialogueTyper>().SetNextKey(nextKey);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDialoguePage(string ID)
    {
        foreach(GameObject page in pages)
        {
            if (page.name == ID)
            {
                page.SetActive(true);
            }
            else
            {
                page.SetActive(false);
            }
        }
    }
}
[Serializable]
public class DialoguePage
{
    public GameObject dObject;
}
