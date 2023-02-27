using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class VolumeClass
{
    public Slider slider;
    public string playerPrefString;
}

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public VolumeClass MasterVolume, MusicVolume, AmbientVolume, DialogueVolume;

    private void Start()
    {
        SoundSettingsStart();
    }

    private void SoundSettingsStart()
    {
        if (PlayerPrefs.HasKey(MasterVolume.playerPrefString) && MasterVolume.slider.value != PlayerPrefs.GetFloat(MasterVolume.playerPrefString))
        {
            MasterVolume.slider.value = PlayerPrefs.GetFloat(MasterVolume.playerPrefString);
        }
        if (PlayerPrefs.HasKey(MusicVolume.playerPrefString) && MusicVolume.slider.value != PlayerPrefs.GetFloat(MusicVolume.playerPrefString))
        {
            MusicVolume.slider.value = PlayerPrefs.GetFloat(MusicVolume.playerPrefString);
        }
        if (PlayerPrefs.HasKey(AmbientVolume.playerPrefString) && AmbientVolume.slider.value != PlayerPrefs.GetFloat(AmbientVolume.playerPrefString))
        {
            AmbientVolume.slider.value = PlayerPrefs.GetFloat(AmbientVolume.playerPrefString);
        }
        if (PlayerPrefs.HasKey(DialogueVolume.playerPrefString) && DialogueVolume.slider.value != PlayerPrefs.GetFloat(DialogueVolume.playerPrefString))
        {
            DialogueVolume.slider.value = PlayerPrefs.GetFloat(DialogueVolume.playerPrefString);
        }
    }


    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterAM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MasterVolume.playerPrefString, sliderValue);
        Debug.Log(PlayerPrefs.GetFloat(MasterVolume.playerPrefString));
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicAM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(MusicVolume.playerPrefString, sliderValue);
        Debug.Log(PlayerPrefs.GetFloat(MusicVolume.playerPrefString));
    }

    public void SetAmbientVolume(float sliderValue)
    {
        audioMixer.SetFloat("AmbientAM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(AmbientVolume.playerPrefString, sliderValue);
        Debug.Log(PlayerPrefs.GetFloat(AmbientVolume.playerPrefString));
    }

    public void SetDialogueVolume(float sliderValue)
    {
        audioMixer.SetFloat("DialogueAM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(DialogueVolume.playerPrefString, sliderValue);
        Debug.Log(PlayerPrefs.GetFloat(DialogueVolume.playerPrefString));
    }
}
