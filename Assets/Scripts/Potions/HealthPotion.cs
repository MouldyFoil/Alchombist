using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] bool tempHeal;
    [SerializeField] bool canceledBySameIDPotions = true;
    [SerializeField] int healAmount = 1;
    [SerializeField] float cooldownOrDuration = 40;
    [SerializeField] int healthPotionID;
    [SerializeField] int additionalMaxHealth;
    [SerializeField] string depletedTag = "DepletedHealthPot";
    Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        if (canceledBySameIDPotions == true)
        {
            DestroySelfIfSamePotionExists();
        }
        else
        {
            StartCoroutine(Heal());
        }
    }

    private void DestroySelfIfSamePotionExists()
    {
        GameObject usedHealthPot = GameObject.FindWithTag(depletedTag);
        if (usedHealthPot && usedHealthPot.GetComponent<HealthPotion>().ReturnID() == healthPotionID)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Heal());
        }
        gameObject.tag = depletedTag;
    }

    private IEnumerator Heal()
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
        yield return new WaitForSeconds(cooldownOrDuration);
        if (tempHeal == true)
        {
            playerHealth.AddOrRemoveTempHealth(-healAmount);
            playerHealth.AddOrRemoveTempMaxHealth(-additionalMaxHealth);
        }
        Destroy(gameObject);
    }
    public int ReturnID() { return healthPotionID; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
