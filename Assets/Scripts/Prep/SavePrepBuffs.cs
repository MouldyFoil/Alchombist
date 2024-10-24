using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrepBuffs : MonoBehaviour
{
    public float speedBuff;
    public int damageBuff;
    public int healthBuff;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType<SavePrepBuffs>().Length > 1)
        {
            Destroy(gameObject);
        }
        
    }
    private void Update()
    {
        if (FindObjectOfType<PlayerMovement>())
        {
            GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
            player.GetComponent<PlayerMovement>().AddMovementSpeed(speedBuff);
            player.GetComponent<AlchemyScript>().AddBuffDamage(damageBuff);
            player.GetComponent<Health>().AddOrRemoveTempMaxHealth(healthBuff);
            player.GetComponent<Health>().AddOrRemoveTempHealth(healthBuff);
            Destroy(gameObject);
        }
    }
}
