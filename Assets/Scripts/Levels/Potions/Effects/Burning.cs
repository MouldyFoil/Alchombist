using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    [SerializeField] float duration = 3;
    [SerializeField] float timeBetweenDamage = 1;
    [SerializeField] int damage = 1;
    Health parentHealth;
    float tickClock;
    float durationClock;
    // Start is called before the first frame update
    void Start()
    {
        parentHealth = GetComponentInParent<Health>();
        List<Burning> allBurnEffects = new List<Burning>(parentHealth.GetComponentsInChildren<Burning>());
        HandleMultipleBurnings(allBurnEffects);
        tickClock = timeBetweenDamage;
        durationClock = duration;
    }

    private void HandleMultipleBurnings(List<Burning> allBurnEffects)
    {
        foreach (Burning b in allBurnEffects)
        {
            if (b != this)
            {
                b.ResetDuration();
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tickClock > 0)
        {
            tickClock -= Time.deltaTime;
        }
        else
        {
            tickClock = timeBetweenDamage;
            parentHealth.AddOrRemoveGeneralHealth(-damage);
        }
        if(durationClock > 0)
        {
            durationClock -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetDuration()
    {
        durationClock = duration;
    }
}
