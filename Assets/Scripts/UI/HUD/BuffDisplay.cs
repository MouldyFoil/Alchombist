using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI damageText;
    // Start is called before the first frame update
    void Start()
    {
        speedText.text = "Speed:";
        damageText.text = "Damage:";
    }
    public void UpdateSpeedUI(float amount)
    {
        if(amount != 0)
        {
            speedText.text = "Speed: +" + amount.ToString();
        }
        else
        {
            speedText.text = "Speed:";
        }
    }
    public void UpdateDamageUI(int amount)
    {
        if(amount != 0)
        {
            damageText.text = "Damage: +" + amount.ToString();
        }
        else
        {
            damageText.text = "Damage:";
        }
    }

}
