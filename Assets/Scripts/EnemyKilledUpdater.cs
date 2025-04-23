using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledUpdater : MonoBehaviour
{
    public int ID;
    CheckpointManager checkpointManager;
    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        if(checkpointManager.ReturnSavedEnemiesKilled() != null)
        {
            foreach (int index in checkpointManager.ReturnSavedEnemiesKilled())
            {
                if (index == ID)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateWasKilled()
    {
        checkpointManager.currentEnemiesKilled.Add(ID);
    }
}
