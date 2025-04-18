using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngredientBag : MonoBehaviour
{
    [SerializeField] int ingredientIndex = 0;
    [SerializeField] UnityEvent pickupEvent;
    [SerializeField] bool destroyIfAlreadyUnlocked = true;
    IngredientRepository ingredientRepository;
    // Start is called before the first frame update
    void Start()
    {
        ingredientRepository = FindObjectOfType<IngredientRepository>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientRepository.ReturnIngredient(ingredientIndex).unlocked == true && destroyIfAlreadyUnlocked)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            ingredientRepository.UnlockIngredient(ingredientIndex);
            pickupEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
