using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public CheckpointData checkpointData;
    [SerializeField] List<EnemyKilledUpdater> enemiesKilledDefault = new List<EnemyKilledUpdater>();
    [SerializeField] List<GameObject> checksCompletedDefault;
    [SerializeField] UnityEvent spawnAtCheckpointEvent;
    [SerializeField] ParticleSystem particles;
    CheckpointManager checkpointManager;
    SaveDataInterface saveData;
    // Start is called before the first frame update
    void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        saveData = FindObjectOfType<SaveDataInterface>();
    }
    private void Update()
    {
        if(checkpointManager == null)
        {
            checkpointManager = FindObjectOfType<CheckpointManager>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkpointManager.FinalizeTempVars();
        checkpointManager.SetCheckpointName(checkpointData.name);
        saveData.AddCheckpointUnlocked(checkpointData);
        particles.Play();
    }
    public string ReturnName() { return name; }
    public List<int> ReturnDefaultEnemiesKilledIDs()
    {
        List<int> enemyIDList = new List<int>();
        foreach (EnemyKilledUpdater enemy in enemiesKilledDefault)
        {
            enemyIDList.Add(enemy.ID);
        }
        return enemyIDList;
    }
    public void InvokeSpawnEvent() { spawnAtCheckpointEvent.Invoke(); }
}
