using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealingPotion : MonoBehaviour
{
    [SerializeField] List<string> sightIDs;
    [SerializeField] float duration;
    [SerializeField] string potionName;
    [SerializeField] List<string> namesOfPotionsToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        if(sightIDs.Count == 0)
        {
            Debug.Log("Sight IDs is empty");
            return;
        }
        if (namesOfPotionsToDestroy.Count > 0)
        {
            DeletePotionsOfName();
        }
        RevealObjects();
    }
    private void DeletePotionsOfName()
    {
        foreach(RevealingPotion potion in FindObjectsOfType<RevealingPotion>())
        {
            if(potion == this)
            {
                continue;
            }
            foreach(string name in namesOfPotionsToDestroy)
            {
                if(potion.ReturnPotionName() == name)
                {
                    Destroy(potion);
                }
            }
        }
    }
    private void RevealObjects()
    {
        foreach (HiddenObject revealObject in FindObjectsOfType<HiddenObject>())
        {
            foreach (string ID in sightIDs)
            {
                if (revealObject.ReturnID() == ID)
                {
                    revealObject.Reveal();
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        foreach(HiddenObject revealObject in FindObjectsOfType<HiddenObject>())
        {
            foreach(string ID in sightIDs)
            {
                if(revealObject.ReturnID() == ID)
                {
                    revealObject.Hide(this);
                }
            }
        }
    }
    public string ReturnPotionName() { return potionName; }
    public List<string> ReturnSightIDs() { return sightIDs; }
}
