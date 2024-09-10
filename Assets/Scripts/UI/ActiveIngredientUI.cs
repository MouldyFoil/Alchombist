using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveIngredientUI : MonoBehaviour
{
    [SerializeField] Sprite[] ingredientSprites;
    [SerializeField] Image[] ingredientsImages;
    IngredientRepository ingredientRepository;
    AlchemyScript playerAlchemy;
    // Start is called before the first frame update
    void Start()
    {
        playerAlchemy = FindObjectOfType<AlchemyScript>();
        ingredientRepository = FindObjectOfType<IngredientRepository>();
        UpdateIngredientUI();
    }
    public void UpdateIngredientUI()
    {
        int index = 0;
        foreach (Image ingredientDisplay in ingredientsImages)
        {
            int ingredient = playerAlchemy.ReturnActiveIngredients(index);
            if(ingredient == 0)
            {
                ingredientDisplay.enabled = false;
            }
            else
            {
                ingredientDisplay.sprite = ingredientRepository.ReturnIngredient(ingredient - 1).ingredientSprite;
                ingredientDisplay.enabled = true;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
