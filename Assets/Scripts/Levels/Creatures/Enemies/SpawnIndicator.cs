using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    [SerializeField] float spawnTime = 1;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] ParticleSystem spawnParticles;
    bool spawned;
    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                enemyToSpawn.SetActive(true);
                Destroy(spawnParticles.gameObject);
                spawned = true;
            }
        }
        if (enemyToSpawn == null)
        {
            Destroy(gameObject);
        }
    }
}
