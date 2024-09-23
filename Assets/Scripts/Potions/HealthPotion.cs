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
        if (usedHealthPot && usedHealthPot.GetComponent<HealthPotion>().ReturnID() == healthPotionType)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Heal());
        }
    }

    private IEnumerator Heal()
    {
        gameObject.tag = depletedTag;
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
