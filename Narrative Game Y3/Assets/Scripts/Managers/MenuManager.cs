using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsUI;

    public void StartButton()
    {
        AudioManager.instance.StopAudio("Menu Music");
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        settingsUI.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
