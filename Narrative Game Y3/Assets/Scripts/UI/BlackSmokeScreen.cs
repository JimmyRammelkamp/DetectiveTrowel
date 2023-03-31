using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmokeScreen : MonoBehaviour
{
    public static BlackSmokeScreen instance;

    [SerializeField] private GameObject smokeEffect;

    private Animator anim;

    void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (BlackSmokeScreen)");
        instance = this;

        anim = GetComponent<Animator>();
        gameObject.GetComponent<Image>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }


    public void TriggerBlackScreen()
    {
        anim.SetTrigger("SmokeScreen");
    }

    public void TriggerSmokeEffect()
    {
        StartCoroutine(TriggerSmoke());
    }

    IEnumerator TriggerSmoke()
    {
        smokeEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        smokeEffect.gameObject.SetActive(false);
    }
}
