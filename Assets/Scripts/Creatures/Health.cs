using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] int maxHealth = 3;
    DisplayHealth playerHealthDisplay;
    int tempMaxHealth;
    int tempHealth;
    int health;
    bool player = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>() != null;
        health = maxHealth;
        damageParticles = GetComponentInChildren<ParticleSystem>();
        if (player == true)
        {
            playerHealthDisplay = FindObjectOfType<DisplayHealth>();
            playerHealthDisplay.UpdateHearts();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public int ReturnHealth() { return health; }
    public int ReturnTempHealth() { return tempHealth; }
    public int ReturnTrueMaxHealth() { return maxHealth; }
    public int ReturnMaxHealth() { return maxHealth + tempMaxHealth; }
    public void AddOrRemoveHealth(int addition)
    {
        if (addition <= 0)
        {
            int damageMinusTempHealth = addition + tempHealth;
            if(damageMinusTempHealth >= 0)
            {
                damageMinusTempHealth = 0;
            }
            if (tempHealth + addition >= 0)
            {
                tempHealth += addition;
            }
            health += damageMinusTempHealth;
        }
        else
        {
            health += addition;
        }
        if(health + tempHealth <= 0)
        {
            Die();
        }
        if (addition <= 0)
        {
            damageParticles.Play();
        }
        if (player == true)
        {
            playerHealthDisplay.UpdateHearts();
        }
    }
    public void AddTempMaxHealth(int addition)
    {
        tempMaxHealth += addition;
        if (health >= maxHealth + tempMaxHealth)
        {
            health = maxHealth + tempMaxHealth;
        }
        playerHealthDisplay.UpdateHearts();
    }
    public void AddTempHealth(int amount)
    {
        tempHealth += amount;
        if (health + tempHealth > maxHealth + tempMaxHealth)
        {
            tempHealth--;
        }
        if(tempHealth <= 0 && tempMaxHealth >= 0)
        {
            tempHealth = 0;
        }
        playerHealthDisplay.UpdateHearts();
    }
    public void Die()
    {
        if(player == false)
        {
            Destroy(gameObject);
        }
        else
        {
            MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
            menuHandler.SwitchMenu("Death");
            menuHandler.PauseGame();
        }
    }
}
