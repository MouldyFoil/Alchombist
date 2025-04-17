using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealingPotion : MonoBehaviour
{
    [SerializeField] List<string> sightIDs;
    // Start is called before the first frame update
    void Start()
    {
        if(sightIDs.Count == 0)
        {
            Debug.Log("Sight IDs is empty");
            return;
        }
        RevealObjects();
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
    public List<string> ReturnSightIDs() { return sightIDs; }
}
