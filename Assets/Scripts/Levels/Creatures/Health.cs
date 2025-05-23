using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] int maxHealth = 3;
    [SerializeField] UnityEvent deathEvent;
    DisplayHealth playerHealthDisplay;
    int tempMaxHealth;
    int tempHealth;
    int health;
    bool player = false;
    bool alreadyDied = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>() != null;
        health = maxHealth;
        if (player == true)
        {
            playerHealthDisplay = FindObjectOfType<DisplayHealth>();
            playerHealthDisplay.UpdateHearts();
        }
    }
    public int ReturnHealth() { return health; }
    public int ReturnTempHealth() { return tempHealth; }
    public int ReturnTrueMaxHealth() { return maxHealth; }
    public int ReturnMaxHealth() { return maxHealth + tempMaxHealth; }
    public void AddOrRemoveGeneralHealth(int interger)
    {
        if (interger < 0) //interger is negative
        {
            int damageMinusTempHealth = interger + tempHealth;
            if(damageMinusTempHealth > 0)
            {
                damageMinusTempHealth = 0;
            }
            AddOrRemoveTempHealth(interger);
            if (health < 0)
            {
                health = 0;
            }
            health += damageMinusTempHealth;
            damageParticles.Play();
            if (health + tempHealth <= 0)
            {
                Die();
            }
            if(player == false && GetComponent<EnemyAI>())
            {
                GetComponent<EnemyAI>().SetPlayerRemembered(true);
            }
        }
        else //interger is positive or 0
        {
            AddOrRemoveTempHealth(interger + health - maxHealth);
            health += interger;
            if(health >= maxHealth)
            {
                health = maxHealth;
            }
        }
        if (player == true)
        {
            playerHealthDisplay.UpdateHearts();
        }
    }
    public void AddOrRemoveTempMaxHealth(int interger)
    {
        if (tempMaxHealth + interger < 0)
        {
            tempMaxHealth = 0;
        }
        else
        {
            tempMaxHealth += interger;
        }
        if(player == true)
        {
            playerHealthDisplay.UpdateHearts();
        }
    }
    public void AddOrRemoveTempHealth(int interger)
    {
        tempHealth += interger;
        if (tempHealth >= tempMaxHealth)
        {
            tempHealth = tempMaxHealth;
        }
        if(tempHealth < 0)
        {
            tempHealth = 0;
        }
        if(player == true)
        {
            playerHealthDisplay.UpdateHearts();
        }
    }
    public void Die()
    {
        if (!alreadyDied)
        {
            alreadyDied = true;
            deathEvent.Invoke();
            if (player == false)
            {
                Destroy(gameObject);
            }
            else
            {
                MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
                menuHandler.DisableEscapeButton();
                menuHandler.SwitchMenu("Death");
                menuHandler.PauseGame();
            }
        }
    }
}
