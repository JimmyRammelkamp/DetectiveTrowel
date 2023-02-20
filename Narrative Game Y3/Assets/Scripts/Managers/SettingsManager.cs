using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class VolumeClass
{
    public Slider slider;
    public float volume;
    public string playerPrefString;
}

public class SettingsManager : MonoBehaviour
{
    AudioMixer audioMixer;

    public VolumeClass MasterVolume, MusicVolume, SoundVolume, DialogueVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey(MasterVolume.playerPrefString) && MasterVolume.volume != PlayerPrefs.GetFloat(MasterVolume.playerPrefString))
            MasterVolume.volume = PlayerPrefs.GetFloat(MasterVolume.playerPrefString);
        if (PlayerPrefs.HasKey(MusicVolume.playerPrefString) && MusicVolume.volume != PlayerPrefs.GetFloat(MusicVolume.playerPrefString))
            MusicVolume.volume = PlayerPrefs.GetFloat(MusicVolume.playerPrefString);
        if (PlayerPrefs.HasKey(SoundVolume.playerPrefString) && SoundVolume.volume != PlayerPrefs.GetFloat(SoundVolume.playerPrefString))
            SoundVolume.volume = PlayerPrefs.GetFloat(SoundVolume.playerPrefString);
        if (PlayerPrefs.HasKey(DialogueVolume.playerPrefString) && DialogueVolume.volume != PlayerPrefs.GetFloat(DialogueVolume.playerPrefString))
            DialogueVolume.volume = PlayerPrefs.GetFloat(DialogueVolume.playerPrefString);
    }
}
