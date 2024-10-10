using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] Sprite fullBonusHeart;
    [SerializeField] Sprite emptyBonusHeart;
    [SerializeField] TextMeshProUGUI healthPastHeartsText;
    Image[] hearts;
    Health playerHealth;
    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        hearts = GetComponentsInChildren<Image>();
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if(hearts == null)
        {
            StartCoroutine(HeartsNullFailsafe());
        }
        else
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < playerHealth.ReturnTrueMaxHealth())
                {
                    if (i < playerHealth.ReturnHealth())
                    {
                        hearts[i].sprite = fullHeart;
                    }
                    else
                    {
                        hearts[i].sprite = emptyHeart;
                    }
                }
                else if (i < playerHealth.ReturnMaxHealth())
                {
                    if (i < playerHealth.ReturnTempHealth() + playerHealth.ReturnTrueMaxHealth())
                    {
                        hearts[i].sprite = fullBonusHeart;
                    }
                    else
                    {
                        hearts[i].sprite = emptyBonusHeart;
                    }
                }
                if (i < playerHealth.ReturnMaxHealth())
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
            if (playerHealth.ReturnHealth() + playerHealth.ReturnTempHealth() > hearts.Length)
            {
                int healthPastHearts = playerHealth.ReturnHealth() + playerHealth.ReturnTempHealth() - hearts.Length;
                healthPastHeartsText.text = "+ " + healthPastHearts;
            }
            else
            {
                healthPastHeartsText.text = "";
            }
        }
    }
    private IEnumerator HeartsNullFailsafe()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        UpdateHearts();
    }
}
