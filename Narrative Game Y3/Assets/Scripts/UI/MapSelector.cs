using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelector : MonoBehaviour, IObjectInteraction, IOutline
{
    [SerializeField] Transform mapPrefab;
    [SerializeField] Transform redCircle;

    private bool isMouseOn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowRedCircle();
    }

    /// <summary>
    ///  Triggers the diorama animation
    /// </summary>
    public void Interact()
    {
        if (DioramaManager.instance.GetAnimationTransform().GetChild(0) == mapPrefab.transform) return;

        NavigatationCamera.instance.ToggleMap();
        DioramaManager.instance.TriggerDioramaAnimation(mapPrefab);
    }

    public void ToggleOutline()
    {
        isMouseOn = true;
    }

    /// <summary>
    ///  Shows the red circle around the map trigger (temporary)
    /// </summary>
    private void ShowRedCircle()
    {
        if (!redCircle) return;

        if (isMouseOn) redCircle.gameObject.SetActive(true);
        else redCircle.gameObject.SetActive(false);

        isMouseOn = false;
    }
}
