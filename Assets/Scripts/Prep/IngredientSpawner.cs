using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] int[] ingredientAmounts;
    [SerializeField] TextMeshProUGUI[] ingredientTexts;
    [SerializeField] GameObject[] ingredients;
    [SerializeField] float ingredientZPos = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        UpdateIngredientInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnIngredient(int ingredientIndex)
    {
        if (ingredientAmounts[ingredientIndex] > 0)
        {
            GameObject ingredient = Instantiate(ingredients[ingredientIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.rotation);
            ingredient.transform.position = new Vector3(ingredient.transform.position.x, ingredient.transform.position.y, ingredientZPos);
            ingredientAmounts[ingredientIndex]--;
        }
        UpdateIngredientInfo();
    }
    private void UpdateIngredientInfo()
    {
        int i = 0;
        foreach(TextMeshProUGUI ingredientText in ingredientTexts)
        {
            ingredientText.text = "x" + ingredientAmounts[i];
            i++;
        }
    }
}
