using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionBook : MonoBehaviour
{
    [SerializeField] GameObject recipeDisplay;
    [SerializeField] Image potionImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI pageText;
    [SerializeField] TextMeshProUGUI potionNumberText;
    [SerializeField] GameObject UIParent;
    [SerializeField] string nextPage = "e";
    [SerializeField] string previousPage = "q";
    [SerializeField] string closeBook = "r";
    PotionRepository potionRepository;
    IngredientRepository ingredientRepository;
    Image[] ingredientSlots;
    int potionNumber;
    int pageNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        potionRepository = FindObjectOfType<PotionRepository>();
        ingredientRepository = FindObjectOfType<IngredientRepository>();
        ingredientSlots = recipeDisplay.GetComponentsInChildren<Image>();
        UpdateBookDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextPage))
        {
            pageNumber++;
            if (pageNumber > potionRepository.ReturnPotionsArrayLength())
            {
                pageNumber = 1;
            }
            UpdateBookDisplay();
        }
        if (Input.GetKeyDown(previousPage))
        {
            pageNumber--;
            if (pageNumber <= 0)
            {
                pageNumber = potionRepository.ReturnPotionsArrayLength();
            }
            UpdateBookDisplay();
        }
        if (Input.GetKeyDown(closeBook))
        {
            UIParent.SetActive(!UIParent.active);
            
        }
    }
    private void UpdateBookDisplay()
    {
        int potionsLeftTillRightPotion = pageNumber;
        potionNumber = 0;
        foreach(Potion potion in potionRepository.ReturnPotions())
        {
            potionNumber++;
            if(potion.discovered == true)
            {
                potionsLeftTillRightPotion--;
                if(potionsLeftTillRightPotion == 0)
                {
                    break;
                }
            }
        }
        potionImage.sprite = potionRepository.ReturnPotion(potionNumber - 1).spriteInBook;
        nameText.text = potionRepository.ReturnPotion(potionNumber - 1).name;
        descriptionText.text = potionRepository.ReturnPotion(potionNumber - 1).description;
        potionNumberText.text = potionNumber.ToString();
        pageText.text = "Pg. " + pageNumber;
        UpdateIngredientDisplay();
    }
    private void UpdateIngredientDisplay()
    {
        char[] charArray = potionRepository.ReturnPotion(potionNumber - 1).ingredientCombo.ToString().ToCharArray();
        List<int> intList = new List<int>();
        int slotsLeft = ingredientSlots.Length;
        int intToAdd;
        foreach(char currentChar in charArray)
        {
            intToAdd = currentChar - '0';
            slotsLeft--;
            intList.Add(intToAdd);
        }
        while(slotsLeft > 0)
        {
            slotsLeft--;
            intList.Add(0);
        }
        int index = 0;
        foreach(Image ingredientSlot in ingredientSlots)
        {
            if(intList.ElementAt(index) == 0)
            {
                ingredientSlot.enabled = false;
            }
            else
            {
                ingredientSlot.sprite = ingredientRepository.ReturnIngredient(intList.ElementAt(index) - 1).ingredientSprite;
                ingredientSlot.enabled = true;
            }
            index++;
        }
    }
}
