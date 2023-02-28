using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Sound test;

    private void Awake()
    {
    }
    private void Start()
    {
        AudioManager.instance.SoundSetUp(test, this.gameObject);
        AudioManager.instance.Play("Test", test);
    }
}
