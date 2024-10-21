using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScroll : MonoBehaviour
{
    [SerializeField] int potionIndex = 0;
    PotionRepository potionRepository;
    // Start is called before the first frame update
    void Start()
    {
        potionRepository = FindObjectOfType<PotionRepository>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            potionRepository.DiscoverPotion(potionIndex);
            Destroy(gameObject);
        }
    }
}
