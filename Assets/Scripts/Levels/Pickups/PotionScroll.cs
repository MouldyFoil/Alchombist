using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScroll : MonoBehaviour
{
    [SerializeField] string potionName;
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
            if (potion.name == potionName && potion.discovered == true)
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
            Destroy(gameObject);
        }
    }
}
