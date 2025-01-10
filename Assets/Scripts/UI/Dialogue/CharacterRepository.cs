using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRepository : MonoBehaviour
{
    //This script is just here to store character information on a prefab and currently does nothing.
    [SerializeField] Character[] characters;
}
[Serializable]
public class Character
{
    public string name;
    public Color[] colors;
    [Header("portrait 0 is default")]
    public Sprite[] portraits;
    public VoiceSound[] voice;
}
[Serializable]
public class VoiceSound
{
    public string tone;
    public AudioClip sound;
}
