using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueCore : MonoBehaviour
{
    [SerializeField] DialoguePage[] dialogueThings;
    [SerializeField] string nextKey = "space";
    // Start is called before the first frame update
    void Start()
    {
        foreach(DialoguePage page in dialogueThings)
        {
            page.dObject.GetComponent<DialogueTyper>().SetNextKey(nextKey);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewDialoguePage(string ID)
    {
        foreach(DialoguePage dialogueThing in dialogueThings)
        {
            if (dialogueThing.dialogueID == ID)
            {
                dialogueThing.dObject.SetActive(true);
            }
            else
            {
                dialogueThing.dObject.SetActive(false);
            }
        }
    }
}
[Serializable]
public class DialoguePage
{
    public string dialogueID;
    public GameObject dObject;
}
