using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] bool startDialogueOnEnable = true;
    [SerializeField] GameObject nextPage;
    [SerializeField] DialogueObject[] dialogueObjects;
    [SerializeField] bool canSkip;
    [SerializeField] bool dialogueOptions;
    [SerializeField] bool automaticallyFlipPage = false;
    [SerializeField] float removeLineSpeed = 0.1f;
    [Header("Leave sound transform empty if player exists.")]
    [SerializeField] Transform soundTransform;
    Transform playerTransform;
    SFXManager sfx;
    int currentIndex = 0;
    string nextInput = "space";
    bool finishedDialogue = false;
    private void OnEnable()
    {
        if (startDialogueOnEnable)
        {
            StartDialogue();
        }
    }
    private void Awake()
    {
        sfx = FindObjectOfType<SFXManager>();
        if (FindObjectOfType<PlayerMovement>())
        {
            playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextInput))
        {
            if (finishedDialogue == true && dialogueOptions == false)
            {
                OpenNextPage();
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
            dialogueObject.objectInQuestion.SetActive(true);
            dialogueObject.startEvent.Invoke();
            dialogueObject.endEvent.Invoke();
            if (dialogueObject.objectInQuestion.GetComponent<TextMeshProUGUI>())
            {
                if(index - 1 >= 0 && dialogueObject.objectInQuestion == dialogueObjects[index - 1].objectInQuestion)
                {
                    dialogueObject.objectInQuestion.GetComponent<TextMeshProUGUI>().text += dialogueObject.text;
                }
                else
                {
                    dialogueObject.objectInQuestion.GetComponent<TextMeshProUGUI>().text = dialogueObject.text;
                }
            }
            else if (dialogueObject.objectInQuestion.GetComponent<TextMeshPro>())
            {
                if (index - 1 >= 0 && dialogueObject.objectInQuestion == dialogueObjects[index - 1].objectInQuestion)
                {
                    dialogueObject.objectInQuestion.GetComponent<TextMeshPro>().text += dialogueObject.text;
                }
                else
                {
                    dialogueObject.objectInQuestion.GetComponent<TextMeshPro>().text = dialogueObject.text;
                }
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
            if (dialogueObject.objectInQuestion.GetComponent<TextMeshProUGUI>())
            {
                dialogueObject.objectInQuestion.GetComponent<TextMeshProUGUI>().text = string.Empty;
            }
            else if (dialogueObject.objectInQuestion.GetComponent<TextMeshPro>())
            {
                dialogueObject.objectInQuestion.GetComponent<TextMeshPro>().text = string.Empty;
            }
            else
            {
                dialogueObject.objectInQuestion.SetActive(false);
            }
        }
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        DialogueObject currentDObject = dialogueObjects[currentIndex];
        yield return new WaitForSeconds(currentDObject.startDelay);
        currentDObject.startEvent.Invoke();
        if (currentDObject.objectInQuestion.GetComponent<TextMeshProUGUI>())
        {
            foreach (char c in currentDObject.text.ToCharArray())
            {
                currentDObject.objectInQuestion.GetComponent<TextMeshProUGUI>().text += c;
                if(!char.IsWhiteSpace(c) && currentDObject.sound)
                {
                    if (soundTransform)
                    {
                        sfx.PlayAudioClip(currentDObject.sound, soundTransform, currentDObject.soundVolume);
                    }
                    else if (playerTransform)
                    {
                        sfx.PlayAudioClip(currentDObject.sound, playerTransform, currentDObject.soundVolume);
                    }
                }
                yield return new WaitForSeconds(currentDObject.waitBetweenCharacters);
            }
        }
        else if (currentDObject.objectInQuestion.GetComponent<TextMeshPro>())
        {
            foreach (char c in currentDObject.text.ToCharArray())
            {
                currentDObject.objectInQuestion.GetComponent<TextMeshPro>().text += c;
                if (!char.IsWhiteSpace(c) && currentDObject.sound)
                {
                    if (soundTransform)
                    {
                        sfx.PlayAudioClip(currentDObject.sound, soundTransform, currentDObject.soundVolume);
                    }
                    else if (playerTransform)
                    {
                        sfx.PlayAudioClip(currentDObject.sound, playerTransform, currentDObject.soundVolume);
                    }
                }
                yield return new WaitForSeconds(currentDObject.waitBetweenCharacters);
            }
        }
        else
        {
            currentDObject.objectInQuestion.SetActive(true);
            if (currentDObject.sound)
            {
                if (soundTransform)
                {
                    sfx.PlayAudioClip(currentDObject.sound, soundTransform, currentDObject.soundVolume);
                }
                else if (playerTransform)
                {
                    sfx.PlayAudioClip(currentDObject.sound, playerTransform, currentDObject.soundVolume);
                }
            }
            yield return new WaitForSeconds(currentDObject.waitBetweenCharacters);
        }
        yield return new WaitForSeconds(currentDObject.endDelay);
        currentDObject.endEvent.Invoke();
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
                OpenNextPage();
            }
        }
    }
    public void OpenNextPage()
    {
        if (nextPage)
        {
            nextPage.SetActive(true);
        }
        gameObject.SetActive(false);
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
    public GameObject objectInQuestion;
    public float waitBetweenCharacters = 0.1f;
    public string text;
    public float endDelay;
    public UnityEvent endEvent;
    public AudioClip sound;
    public float soundVolume = 0.15f;
}
