using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueCore : MonoBehaviour
{
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI mainDialogueBox;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] GameObject typerObject;
    GameObject currentTyper;
    bool dialogueEventsOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateTyper(TextMeshProUGUI typerTargetText)
    {
        currentTyper = Instantiate(typerObject);
    }
}
[Serializable]
public class DialogueText
{
    public string text;
    public Color color;
    public TMP_FontAsset font;
}
