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
    Potion potionData;
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
            if (pageNumber > potionRepository.ReturnPotionsDiscovered())
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
                pageNumber = potionRepository.ReturnPotionsDiscovered();
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
        int potionNum = 0;
        int renameInt = 0;
        foreach(Potion potion in potionRepository.ReturnPotions())
        {
            potionNum++;
            if (potion.discovered)
            {
                renameInt++;
                if(renameInt == pageNumber)
                {
                    potionData = potion;
                    break;
                }
            }
        }
        potionImage.sprite = potionData.spriteInBook;
        nameText.text = potionData.name;
        descriptionText.text = potionData.description;
        potionNumberText.text = "potion #" + potionNum;
        pageText.text = "Pg. " + pageNumber;
        UpdateIngredientDisplay();
    }
    private void UpdateIngredientDisplay()
    {
        char[] charArray = potionData.ingredientCombo.ToString().ToCharArray();
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
