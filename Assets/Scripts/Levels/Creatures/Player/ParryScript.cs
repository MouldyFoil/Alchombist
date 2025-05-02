using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    public Color[] parryTypes;
    [SerializeField] float parryTime = 0.2f;
    [SerializeField] float parryRefreshTimeOnSuccess = 0.3f;
    [SerializeField] float hitstunTime = 0.2f;
    SpriteRenderer sprite;
    int currentParryType;
    float parryClock = 0;
    [SerializeField] GameObject hitStunUI;
    [SerializeField] AudioClip parrySound;
    [SerializeField] float parryVolume = 0.5f;
    SFXManager sfx;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sfx = FindObjectOfType<SFXManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (parryClock > 0)
        {
            parryClock -= Time.deltaTime;
        }
        sprite.enabled = parryClock > 0;
    }
    public void ActivateParry(int parryType)
    {
        currentParryType = parryType - 1;
        parryClock = parryTime;
        Color parryColorFinal = parryTypes[currentParryType];
        sprite.color = parryColorFinal;
    }
    public void ParrySuccessFX()
    {
        StartCoroutine(FX());
        parryClock = parryRefreshTimeOnSuccess;
    }
    private IEnumerator FX()
    {
        Time.timeScale = 0;
        hitStunUI.SetActive(true);
        if(parrySound != null)
        {
            sfx.PlayAudioClip(parrySound, transform, parryVolume);
        }
        else { Debug.Log("parry sound is null"); }
        yield return new WaitForSecondsRealtime(hitstunTime);
        Time.timeScale = 1;
        hitStunUI.SetActive(false);
    }
    public bool ReturnParryActive() { return sprite.enabled; }
    public int ReturnParryType() { return currentParryType; }
}
