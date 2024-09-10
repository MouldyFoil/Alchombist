using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] bool tempHeal;
    [SerializeField] bool canceledBySameIDPotions = true;
    [SerializeField] int healAmount = 1;
    [SerializeField] float cooldownOrDuration = 40;
    [SerializeField] int healthPotionType;
    [SerializeField] int additionalMaxHealth;
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        if(canceledBySameIDPotions == true)
        {
            DestroySelfIfSamePotionExists();
        }
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        StartCoroutine(Heal());
    }

    private void DestroySelfIfSamePotionExists()
    {
        foreach (HealthPotion potion in FindObjectsOfType<HealthPotion>())
        {
            if (potion.gameObject.GetInstanceID() != gameObject.GetInstanceID() && potion.ReturnID() == healthPotionType)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Heal()
    {
        if (tempHeal == true)
        {
            playerHealth.AddTempMaxHealth(additionalMaxHealth);
            playerHealth.AddTempHealth(healAmount);
        }
        else
        {
            playerHealth.AddOrRemoveHealth(healAmount);
        }
        yield return new WaitForSeconds(cooldownOrDuration);
        if (tempHeal == true)
        {
            playerHealth.AddTempHealth(-healAmount);
            playerHealth.AddTempMaxHealth(-additionalMaxHealth);
        }
        Destroy(gameObject);
    }
    public int ReturnID() { return healthPotionType; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
