using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLight : MonoBehaviour
{
    public static EnvironmentLight instance;

    Light directionalLight;

    float defaultIntensity;

    void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (EnvironmentLight)");
        instance = this;
    }

        void Start()
    {
        directionalLight = GetComponent<Light>();
        defaultIntensity = directionalLight.intensity;
    }

    public void ChangeToDefaultIntensity()
    {
        StartCoroutine(ChangeLightIntensityIE(defaultIntensity));
    }

    public void ChangeLightIntensity(float _intensity)
    {
        StartCoroutine(ChangeLightIntensityIE(_intensity));
    }

    IEnumerator ChangeLightIntensityIE(float _intensity)
    {
        float lerp = 0;
        float startIntenstiy = directionalLight.intensity;
        float endIntensity = _intensity;

        while (lerp < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            lerp += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(startIntenstiy, endIntensity, lerp);
        }
    }
}
