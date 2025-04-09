using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    [SerializeField] float spawnTime = 1;
    [SerializeField] GameObject enemyToSpawn;
    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (enemyToSpawn == null)
        {
            Destroy(gameObject);
        }
        else if (spawnTime <= 0)
        {
            enemyToSpawn.SetActive(true);
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
