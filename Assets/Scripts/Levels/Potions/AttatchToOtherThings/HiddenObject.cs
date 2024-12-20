using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] string revealID;
    [Header("Extra Variables")]
    [SerializeField] Behaviour[] extraComponentsToDisable;
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject hintOrReplacementObjcet;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }
    public void Reveal()
    {
        sprite.enabled = true;
        foreach (Behaviour component in extraComponentsToDisable)
        {
            component.enabled = true;
        }
    }
    public void Hide(RevealingPotion potionCalling)
    {
        //checks to make sure that no other potions that should reveal object are active and then hides
        RevealingPotion[] revealingPotions = FindObjectsOfType<RevealingPotion>();
        foreach(RevealingPotion revealingPotion in revealingPotions)
        {
            if(revealingPotion == potionCalling)
            {
                continue;
            }
            foreach (string sightID in revealingPotion.ReturnSightIDs())
            {
                if(sightID == revealID)
                {
                    return;
                }
            }
        }
        sprite.enabled = false;
        foreach (Behaviour component in extraComponentsToDisable)
        {
            component.enabled = false;
        }
    }
    public string ReturnID() { return revealID; }
}
