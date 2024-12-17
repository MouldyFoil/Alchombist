using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalPotion : MonoBehaviour
{
    [SerializeField] float multiplier = 2;
    [SerializeField] int minSucceed = 1;
    [SerializeField] int numberPool = 10;
    public float RollForMultiplier()
    {
        int numberRolled = Random.Range(1, numberPool);
        if(numberRolled <= minSucceed)
        {
            return multiplier;
        }
        return 1;
    }
}
