using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] DialogueObject[] dialogueObjects;
    [SerializeField] string nextPageID;
    [SerializeField] bool canSkip;
    [SerializeField] bool dialogueOptions;
    [SerializeField] bool automaticallyFlipPage = false;
    [SerializeField] float removeLineSpeed = 0.1f;
    int currentIndex = 0;
    DialogueCore dialogueCore;
    string nextInput = "space";
    bool finishedDialogue = false;
    private void OnEnable()
    {
        StartDialogue();
    }
    private void Awake()
    {
        dialogueCore = FindObjectOfType<DialogueCore>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextInput))
        {
            if (finishedDialogue == true && dialogueOptions == false)
            {
                dialogueCore.NewDialoguePage(nextPageID);
            }
            else if (canSkip == true)
            {
                SkipDialogue();
            }
        }
    }
    private void SkipDialogue()
    {
        StopAllCoroutines();
        int index = 0;
        foreach(DialogueObject dialogueObject in dialogueObjects)
        {
            dialogueObject.dGameObject.SetActive(true);
            if (dialogueObject.dGameObject.GetComponent<TextMeshProUGUI>())
            {
                dialogueObject.dGameObject.GetComponent<TextMeshProUGUI>().text = dialogueObjects[index].text;
            }
            index++;
        }
        finishedDialogue = true;
    }
    public void SetNextKey(string newKey)
    {
        nextInput = newKey;
    }
    public void ChangeRemoveSpeed(float speed)
    {
        removeLineSpeed = speed;
    }
    public void RemoveLine(GameObject text)
    {
        StartCoroutine(RemoveLineCoroutine(text.GetComponent<TextMeshProUGUI>(), removeLineSpeed));
    }
    private void StartDialogue()
    {
        finishedDialogue = false;
        currentIndex = 0;
        foreach(DialogueObject dialogueObject in dialogueObjects)
        {
            if (dialogueObject.dGameObject.GetComponent<TextMeshProUGUI>())
            {
                dialogueObject.dGameObject.GetComponent<TextMeshProUGUI>().text = string.Empty;
            }
            else
            {
                dialogueObject.dGameObject.SetActive(false);
            }
        }
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        dialogueObjects[currentIndex].startEvent.Invoke();
        yield return new WaitForSeconds(dialogueObjects[currentIndex].startDelay);
        if (dialogueObjects[currentIndex].dGameObject.GetComponent<TextMeshProUGUI>())
        {
            foreach (char c in dialogueObjects[currentIndex].text.ToCharArray())
            {
                dialogueObjects[currentIndex].dGameObject.GetComponent<TextMeshProUGUI>().text += c;
                yield return new WaitForSeconds(dialogueObjects[currentIndex].waitBetweenCharacters);
            }
        }
        else
        {
            dialogueObjects[currentIndex].dGameObject.SetActive(true);
            yield return new WaitForSeconds(dialogueObjects[currentIndex].waitBetweenCharacters);
        }
        yield return new WaitForSeconds(dialogueObjects[currentIndex].endDelay);
        dialogueObjects[currentIndex].endEvent.Invoke();
        currentIndex++;
        if(currentIndex < dialogueObjects.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            finishedDialogue = true;
            if (automaticallyFlipPage)
            {
                dialogueCore.NewDialoguePage(nextPageID);
            }
        }
    }
    IEnumerator RemoveLineCoroutine(TextMeshProUGUI tmp, float timeBetweenRemoval)
    {
        foreach(char c in tmp.text.ToCharArray())
        {
            tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
            yield return new WaitForSeconds(timeBetweenRemoval);
        }
    }
}
[Serializable]
public class DialogueObject
{
    public float startDelay;
    public UnityEvent startEvent;
    public GameObject dGameObject;
    public float waitBetweenCharacters = 0.1f;
    public string text;
    public float endDelay;
    public UnityEvent endEvent;
}
