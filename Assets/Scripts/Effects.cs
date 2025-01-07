using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] Color[] colors;
    SpriteRenderer selectedSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSelectedSprite(int index)
    {
        selectedSprite = sprites[index];
    }
    public void SetSelectedSpriteColor(int index)
    {
        selectedSprite.color = colors[index];
    }
    public void PlayParticles(int index)
    {
        particleSystems[index].Play();
    }
    public void StopParticles(int index)
    {
        particleSystems[index].Stop();
    }
}
