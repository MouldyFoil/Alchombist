using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrepBuffs : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buffText;
    [SerializeField] TextMeshProUGUI statOverviewText;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] int health;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStatUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseStat(string statName, float amount)
    {
        switch (statName)
        {
            case "Speed":
                speed += amount;
                break;
            case "Damage":
                damage += (int)amount;
                break;
            case "Health":
                health += (int)amount;
                break;
        }
        UpdateStatUI();
    }
    public void SaveBuffs()
    {
        SavePrepBuffs saves = FindObjectOfType<SavePrepBuffs>();
        saves.speedBuff = speed;
        saves.damageBuff = damage;
        saves.healthBuff = health;
    }
    private void UpdateStatUI()
    {
        buffText.text = "Buffs: " + "Speed: +" + speed + " Damage +" + damage + " Health: +" + health;
        statOverviewText.text = "Your potion gives you an extra: " + speed + " Speed, " + damage + " Damage, " + " and " + health + " Health";
    }
}
