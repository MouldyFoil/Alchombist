using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadSavedScene()
    {
        SceneManager.LoadScene(FindObjectOfType<SaveNextScene>().sceneName);
    }
    public void SetSavedScene(string name)
    {
        FindObjectOfType<SaveNextScene>().sceneName = name;
    }
    public void AddExtraVar(string name)
    {
        FindObjectOfType<SaveNextScene>().AddSavedVar(name);
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        Debug.Log("reloaded");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public string ReturnCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
