using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float waitTimeBetweenCharacters;
    [SerializeField] string text;
    [SerializeField] UnityEvent endEvent;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDialogueTextObject(TextMeshProUGUI text)
    {
        dialogueText = text;
    }
    public void StartDialogue()
    {
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(waitTimeBetweenCharacters);
        }
        endEvent.Invoke();
        Destroy(gameObject);
    }
    public void DestroySelf()
    {
        StopAllCoroutines();
        dialogueText.text = text;
        Destroy(gameObject);
    }
}
