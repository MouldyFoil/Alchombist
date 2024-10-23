using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveNextScene : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(FindObjectsOfType<SaveNextScene>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
