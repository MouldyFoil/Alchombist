using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBag : MonoBehaviour
{
    [SerializeField] int ingredientIndex = 0;
    IngredientRepository ingredientRepository;
    // Start is called before the first frame update
    void Start()
    {
        ingredientRepository = FindObjectOfType<IngredientRepository>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientRepository.ReturnIngredient(ingredientIndex).unlocked == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            ingredientRepository.UnlockIngredient(ingredientIndex);
            Destroy(gameObject);
        }
    }
}
