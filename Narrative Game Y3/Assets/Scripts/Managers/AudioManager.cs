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

    public void SoundSetUp(Sound sound, GameObject sourceObject = null, AudioClip clip = null)     //  SoundSetUp allows other Scripts to access this fuction to add the AudioSource to other objects instead of the Audio Manager
    {
        if (sourceObject == null) sound.source = gameObject.AddComponent<AudioSource>();
        else
        {
            if (sourceObject.GetComponent<AudioSource>() == null) 
                sound.source = sourceObject.AddComponent<AudioSource>();
            else sound.source = sourceObject.GetComponent<AudioSource>();
        }

        if (clip == null) sound.source.clip = sound.clip;
        else sound.source.clip = clip;

        sound.source.outputAudioMixerGroup = sound.outputAudioMixerGroup;

        sound.source.volume = sound.volume;

        sound.source.pitch = sound.pitch;

        sound.source.spatialBlend = sound.spacialBlend;

        sound.source.loop = sound.loop;
    }

    public void Play(string name = null, Sound s = null)
    {
        if (s == null)
        {
            if (name == null)
            {
                Debug.LogError("name string is null, unable to search for Sound Class in 'sounds' array!");
                return;
            }
            s = Array.Find(sounds, sound => sound.name == name);
        }

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //  Plays Audio clip from Sound class "s"
        s.source.Play();
    }

    public void Stop(string name = null, Sound s = null)
    {
        //  If "_s" is null then 
        if (s == null)
        {
            if (name == null)
            {
                Debug.LogError("name string is null, unable to search for Sound Class in 'sounds' array!");
                return;
            }
            s = Array.Find(sounds, sound => sound.name == name);
        }

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //  Stops Audio clip from Sound class "s"
        s.source.Stop();
    }
}
