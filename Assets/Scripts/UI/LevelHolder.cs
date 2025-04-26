using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI checkpointText;
    [SerializeField] string noCheckpointText = "Start at beginning";
    [SerializeField] int checkpointIndex = -1;
    [HideInInspector]
    string sceneName;
    List<CheckpointData> checkpoints;
    SaveDataInterface saveData;
    SceneManagement sceneManager;

    private void Awake()
    {
        checkpointIndex = -1;
        sceneManager = FindObjectOfType<SceneManagement>();
        saveData = FindObjectOfType<SaveDataInterface>();
    }
    public void NextCheckpoint()
    {
        checkpointIndex++;
        if (checkpointIndex >= checkpoints.Count)
        {
            checkpointIndex = -1;
        }
        UpdateCheckpointText();
    }
    public void PreviousCheckpoint()
    {
        checkpointIndex--;
        if(checkpointIndex < -1)
        {
            checkpointIndex = checkpoints.Count - 1;
        }
        UpdateCheckpointText();
    }
    public void SetScene(string name)
    {
        sceneName = name;
        checkpoints = saveData.FindLevelDataByName(sceneName).checkPointData;
        UpdateLevelText();
        UpdateCheckpointText();
    }
    private void UpdateLevelText()
    {
        levelText.text = sceneName;
    }
    private void UpdateCheckpointText()
    {
        if(checkpointIndex == -1)
        {
            checkpointText.text = noCheckpointText;
        }
        else
        {
            checkpointText.text = checkpoints[checkpointIndex].name;
        }
    }
    public void SaveHeldSceneToManager()
    {
        sceneManager.SetSavedScene(sceneName);
    }
    public void SendCheckpointInfoToManager()
    {
        if(checkpointIndex >= 0)
        {
            CheckpointManager manager = FindObjectOfType<CheckpointManager>();
            manager.SetCheckpointName(checkpoints[checkpointIndex].name);
            manager.UseDefaultCheckpointCompletionVars();
        }
    }
}
