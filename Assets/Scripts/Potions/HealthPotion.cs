using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] bool tempHeal;
    [SerializeField] int healAmount = 1;
    [SerializeField] int healthPotionID;
    [SerializeField] int additionalMaxHealth;
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        Heal();
    }

    private void Heal()
    {
        if (tempHeal == true)
        {
            playerHealth.AddOrRemoveTempMaxHealth(additionalMaxHealth);
            playerHealth.AddOrRemoveTempHealth(healAmount);
        }
        else
        {
            playerHealth.AddOrRemoveGeneralHealth(healAmount);
        }
    }
    private void OnDestroy()
    {
        if (tempHeal == true)
        {
            playerHealth.AddOrRemoveTempHealth(-healAmount);
            playerHealth.AddOrRemoveTempMaxHealth(-additionalMaxHealth);
        }
    }
    public int ReturnID() { return healthPotionID; }
}
