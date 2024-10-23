using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuffs : MonoBehaviour
{
    public float speedBuff;
    public float damageBuff;
    public int healthBuff;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType<SaveNextScene>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
