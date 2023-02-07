using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DioramaManager : MonoBehaviour
{
    public static DioramaManager instance;

    [SerializeField] private Transform animationTransform;

    private Animator anim;
    private Transform newDiorama;

    public Transform GetAnimationTransform() { return animationTransform; }

    void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (DioramaManager)");
        instance = this;
    }

    private void Start()
    {
        Initalize();
    }
    private void Initalize()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    ///  Set the new Diorama and starts the changing Coroutine 
    /// </summary>
    public void TriggerDioramaAnimation(Transform _newDiorama)
    {
        newDiorama = _newDiorama;
        StartCoroutine(DioramaAnimation());
    }

    /// <summary>
    ///  Triggers the Diorama changing animation after the Map -> Table camera transition ends
    /// </summary>
    private IEnumerator DioramaAnimation()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        while (NavigationCamera.instance.IsCameraTransitionOver()) yield return null;

        anim.SetTrigger("ChangeDiorama");
    }

    /// <summary>
    ///  When the Diarama is under the table the old diorama disabled and the new enabled
    /// </summary>
    public void ChangeDiorama()
    {
        animationTransform.GetChild(0).gameObject.SetActive(false);
        animationTransform.GetChild(0).SetParent(transform);

        newDiorama.SetParent(animationTransform);
        newDiorama.gameObject.SetActive(true);
    }

    /// <summary>
    ///  Checks if the animation is over
    /// </summary>
    public bool IsDioramaAnimationOver()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsTag("DioramaAnimation");
    }
}
