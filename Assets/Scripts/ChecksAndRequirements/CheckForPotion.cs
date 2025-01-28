using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckForPotion : MonoBehaviour
{
    [SerializeField] string potionName;
    [SerializeField] GeneralRequirementScript generalRequirementScript;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GenericPotionProperties>())
        {
            foreach(GenericPotionProperties potion in FindObjectsOfType<GenericPotionProperties>())
            {
                if (potion.ReturnPotionName() == potionName && !potion.CheckChecked())
                {
                    potion.GetChecked();
                    generalRequirementScript.TickUpRequirement();
                }
            }
        }
    }
}
