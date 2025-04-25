using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [HideInInspector]
    public List<int> currentEnemiesKilled = new List<int>();
    List<int> enemiesKilledSaved = new List<int>();
    [HideInInspector]
    public List<int> currentIngredientsCollected = new List<int>();
    List<int> ingredientsCollectedSaved = new List<int>();
    Vector2 checkpointPos;
    GameObject player;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        foreach(CheckpointManager checkpointManager in FindObjectsOfType<CheckpointManager>())
        {
            if(checkpointManager != this)
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
        if(checkpointPos != null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
            player.transform.position = checkpointPos;
        }
        ResetTempData();
    }
    public void ResetTempData()
    {
        currentEnemiesKilled = new List<int>();
        currentIngredientsCollected = new List<int>();
    }
    public void ResetLevelData()
    {
        enemiesKilledSaved = new List<int>();
        ingredientsCollectedSaved = new List<int>();
        checkpointPos = new Vector2();
    }
    public void FinalizeTempVars()
    {
        if(currentEnemiesKilled != null)
        {
            enemiesKilledSaved.AddRange(currentEnemiesKilled);
        }
        if(currentIngredientsCollected != null)
        {
            ingredientsCollectedSaved.AddRange(currentIngredientsCollected);
        }
    }
    public List<int> ReturnSavedEnemiesKilled()
    {
        return enemiesKilledSaved;
    }
    public void SetCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
}
