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
    public void PlayAudioClip(AudioClip audio, Transform spawnTransform, float volume, float priority = 128)
    {
        
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audio;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlayRandomAudioClip(AudioClip[] audioArray, Transform spawnTransform, float volume)
    {
        if(audioArray.Length < 1)
        {
            Debug.Log("Audio is null");
            return;
        }
        AudioClip audio = audioArray[Random.Range(1, audioArray.Length) - 1];

        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audio;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
