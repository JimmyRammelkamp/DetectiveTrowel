using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NavigationCamera : MonoBehaviour
{
    public static NavigationCamera instance;

    [SerializeField] CinemachineVirtualCamera tableCamera;
    [SerializeField] CinemachineVirtualCamera mapCamera;
    [SerializeField] CinemachineVirtualCamera dioramaCamera;
    [SerializeField] CinemachineVirtualCamera newspaperCamera;

    bool isMapActive = false;

    public CinemachineVirtualCamera GetTableCamera() { return tableCamera; }
    public CinemachineVirtualCamera GetMapCamera() { return mapCamera; }
    public CinemachineVirtualCamera GetDioramaCamera() { return dioramaCamera; }
    public CinemachineVirtualCamera GetNewspaperCamera() { return newspaperCamera; }

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (NavigationCamera)");
        instance = this;
    }

    private void Start()
    {
        ActivateMapCamera(tableCamera);
    }

    /// <summary>
    /// Transition between the Table and the Map camera
    /// </summary>
    public void ToggleMap()
    {
        if (GameManager.instance.GetStatus() != GameManager.GameStatus.Map) GameManager.instance.SetStatus(GameManager.GameStatus.Map);
        else GameManager.instance.SetStatus(GameManager.GameStatus.Table);
    }

    /// <summary>
    /// Disable all cameras
    /// </summary>
    private void DisableAllCamera()
    {
        mapCamera.Priority = 0;
        tableCamera.Priority = 0;
        dioramaCamera.Priority = 0;
        newspaperCamera.Priority = 0;
    }

    /// <summary>
    /// Activate the new camera
    /// </summary>
    public void ActivateMapCamera(CinemachineVirtualCamera _VCamera)
    {
        DisableAllCamera();

        _VCamera.Priority = 1;
    }

    /// <summary>
    /// Checks if the camera transition is over or not
    /// </summary>
    public bool IsCameraTransitionOver()
    {
        return Camera.main.GetComponent<CinemachineBrain>().IsBlending;
    }
}
