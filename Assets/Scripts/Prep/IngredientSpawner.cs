using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] int[] ingredientAmounts;
    [SerializeField] GameObject[] ingredients;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnIngredient(int ingredientIndex)
    {
        
        if (ingredientAmounts[ingredientIndex] > 0)
        {
            var ingredient = Instantiate(ingredients[ingredientIndex], Camera.main.ScreenToWorldPoint(Input.mousePosition), gameObject.transform.rotation);
            ingredientAmounts[ingredientIndex]--;
        }
    }
}
