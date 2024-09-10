using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangePotion : MonoBehaviour
{
    [SerializeField] bool changeMovement;
    [SerializeField] bool changeAttack;
    [SerializeField] int attackChange = 2;
    [SerializeField] float movementChange = 10;
    [SerializeField] float duration = 3;
    PlayerMovement playerMovement;
    AlchemyScript playerAlchemy;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAlchemy = FindObjectOfType<AlchemyScript>();
        StartCoroutine(DoEffects());
    }

    private IEnumerator DoEffects()
    {
        if(changeMovement == true)
        {
            playerMovement.AddMovementSpeed(movementChange);
        }
        if(changeAttack == true)
        {
            playerAlchemy.AddBuffDamage(attackChange);
        }
        yield return new WaitForSeconds(duration);
        if(changeMovement == true)
        {
            playerMovement.AddMovementSpeed(-movementChange);
        }
        if(changeAttack == true)
        {
            playerAlchemy.AddBuffDamage(-attackChange);
        }
        Destroy(gameObject);
    }
}
