using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledUpdater : MonoBehaviour
{
    [HideInInspector]
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
    private void OnValidate()
    {
        ID = Random.Range(0, 999999);
        UniqueIDFailsafe();
    }
    private void UniqueIDFailsafe()
    {
        loopStart:
        foreach (EnemyKilledUpdater enemy in FindObjectsOfType<EnemyKilledUpdater>())
        {
            if (enemy != this && enemy.ID == ID)
            {
                ID = Random.Range(0, 999999);
                goto loopStart;
            }
        }
    }
}
