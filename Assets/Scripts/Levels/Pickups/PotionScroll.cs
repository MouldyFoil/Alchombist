using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PotionScroll : MonoBehaviour
{
    [SerializeField] string potionName;
    [SerializeField] UnityEvent pickupEvent;
    [SerializeField] bool destroyIfAlreadyUnlocked = true;
    PotionRepository potionRepository;
    // Start is called before the first frame update
    void Start()
    {
        potionRepository = FindObjectOfType<PotionRepository>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Potion potion in potionRepository.ReturnPotions())
        {
            if (potion.name == potionName && potion.discovered == true && destroyIfAlreadyUnlocked)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            potionRepository.DiscoverPotionByName(potionName);
            pickupEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
