using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;


//to use FindObjectOfType<SoundManager>().Play("");
public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    
    private void Awake()
    {
        foreach (Sound S in sounds)
        {
            S.source = gameObject.AddComponent<AudioSource>();
            S.source.clip = S.clip;
            S.source.volume = S.volume;
        }
    }
    private void Update()
    {
        foreach (Sound S in sounds)
        {
            S.source.volume = S.volume;
        }
    }
    public float CheckLength(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {

                return sounds[i].source.clip.length;
            }
        }
        return 0;
    }
    public void Play(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {

                sounds[i].source.Play();
            }
        }
    }

}
