using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepIngredientPickup : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] int amount;
    SaveDataInterface saveDataInterface;
    // Start is called before the first frame update
    void Start()
    {
        saveDataInterface = FindObjectOfType<SaveDataInterface>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            saveDataInterface.AddOrRemoveIngredientAmount(index, amount);
            saveDataInterface.SaveIngredientAmounts();
            Destroy(gameObject);
        }
    }
}
