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
    [SerializeField] float damage;
    [SerializeField] int bonusHealth;
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
                damage += amount;
                break;
            case "Health":
                bonusHealth += (int)amount;
                break;
        }
        UpdateStatUI();
    }
    private void UpdateStatUI()
    {
        buffText.text = "Buffs: " + "Speed: +" + speed + " Damage +" + damage + " Health: +" + bonusHealth;
        statOverviewText.text = "Your potion gives you an extra: " + speed + " Speed, " + damage + " Damage, " + " and " + bonusHealth + " Health";
    }
}
