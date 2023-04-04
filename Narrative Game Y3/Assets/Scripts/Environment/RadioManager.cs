using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour, IObjectInteraction
{
    [SerializeField] AudioClip[] AvailableSongs;
    [SerializeField] AudioSource radioAudioSource;
    [SerializeField] AudioClip interactSound;
    int songIndex = 0;
    int maxSongindex;
    bool isOn = true;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        maxSongindex = AvailableSongs.Length;
    }

    public void Interact()
    {
        isOn = !isOn;

        if(isOn) {
            songIndex += 1;
            songIndex %= maxSongindex;

            animator.enabled = true;
            radioAudioSource.clip = AvailableSongs[songIndex];
            radioAudioSource.Play();
        }
        else
        {
            radioAudioSource.Stop();
            animator.enabled = false;
        }
        radioAudioSource.PlayOneShot(interactSound, 0.1f);
    }

    public bool isObjectActive()
    {
        return true;
    }
}
