using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds) SoundSetUp(s);
    }

    public void SoundSetUp(Sound sound, GameObject sourceObject = null)     //  SoundSetUp allows other Scripts to access this fuction to add the AudioSource to other objects instead of the Audio Manager
    {
        if (sourceObject == null) sound.source = gameObject.AddComponent<AudioSource>();
        else
        {
            if (sourceObject.GetComponent<AudioSource>() == null) 
                sound.source = sourceObject.AddComponent<AudioSource>();
            else sound.source = sourceObject.GetComponent<AudioSource>();
        }

        sound.source.clip = sound.clip;

        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;

        sound.source.spatialBlend = sound.spacialBlend;

        sound.source.loop = sound.loop;
    }

    public void Play(string name, Sound _s = null)
    {
        Sound s;
        if (_s == null) s = Array.Find(sounds, sound => sound.name == name);
        else s = _s;

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name, Sound _s = null)
    {
        Sound s;
        if (_s == null) s = Array.Find(sounds, sound => sound.name == name);
        else s = _s;

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
