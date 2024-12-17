using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [SerializeField] string revealID;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Reveal()
    {
        sprite.enabled = true;
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
    }
    public string ReturnID() { return revealID; }
}
