using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    [SerializeField] string currentCheckpointName;
    [SerializeField] Checkpoint currentCheckpoint;


    //vars that have "saved" mean that they are saved for when the player reloads to a checkpoint
    [HideInInspector]
    public List<int> currentEnemiesKilled = new List<int>();
    List<int> enemiesKilledSaved = new List<int>();

    [HideInInspector]
    public List<int> pickupsCollected = new List<int>();
    List<int> pickupsCollectedSaved = new List<int>();

    GameObject player;
    //useDefault is for starting a level from main menu on a specific checkpoint and makes the script use said checkpoint's default variables 
    bool useDefault = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
        foreach (CheckpointManager checkpointManager in FindObjectsOfType<CheckpointManager>())
        {
            if (checkpointManager != this)
            {
                checkpointManager.SceneReloadActions();
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SceneReloadActions()
    {
        checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
        FindCurrentCheckpoint();
        if (currentCheckpoint)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
            player.transform.position = currentCheckpoint.transform.position;
            currentCheckpoint.InvokeSpawnEvent();
        }
        if (currentCheckpoint && useDefault)
        {
            enemiesKilledSaved = currentCheckpoint.ReturnDefaultEnemiesKilledIDs();
            useDefault = false;
        }
        DestroyEnemiesKilled();
        ResetTempData();
    }
    private void FindCurrentCheckpoint()
    {
        foreach(Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.checkpointData.name == currentCheckpointName)
            {
                currentCheckpoint = checkpoint;
            }
        }
    }
    private void DestroyEnemiesKilled()
    {
        foreach(EnemyKilledUpdater enemy in FindObjectsOfType<EnemyKilledUpdater>())
        {
            foreach(int ID in enemiesKilledSaved)
            {
                if(ID == enemy.ID)
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }
    public void ResetTempData()
    {
        currentEnemiesKilled = new List<int>();
        pickupsCollected = new List<int>();
    }
    public void ResetLevelData()
    {
        enemiesKilledSaved = new List<int>();
        pickupsCollectedSaved = new List<int>();
        currentCheckpoint = null;
    }
    public void FinalizeTempVars()
    {
        if(currentEnemiesKilled != null)
        {
            enemiesKilledSaved.AddRange(currentEnemiesKilled);
        }
        if(pickupsCollected != null)
        {
            pickupsCollectedSaved.AddRange(pickupsCollected);
        }
    }
    public List<int> ReturnSavedEnemiesKilled()
    {
        return enemiesKilledSaved;
    }
    public void SetCheckpointName(string checkpointName)
    {
        currentCheckpointName = checkpointName;
    }
    public void UseDefaultCheckpointCompletionVars()
    {
        useDefault = true;
    }
}
