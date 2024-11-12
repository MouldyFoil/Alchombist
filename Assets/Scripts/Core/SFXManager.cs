using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundObject;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType<SFXManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
    public void PlayAudioClip(AudioClip audio, Transform spawnTransform, float volume)
    {
        
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audio;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
