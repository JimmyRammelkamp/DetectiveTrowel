using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private RectTransform statusIcons;

    private GameManager.GameStatus tempStatus; // for updating the UI elements


    public RectTransform GetStatusIconParent() { return statusIcons; }

    void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (HUDManager)");
        instance = this;
    }

    private void Start()
    {
        Initalize();
    }

    private void Update()
    {
        ActivateUIElementsAccordingToGameStatus();
    }

    private void Initalize()
    {
        statusIcons.gameObject.SetActive(false);
    }

    /// <summary>
    ///  Disable every UI elements
    /// </summary>
    private void DisableUIElements()
    {
        statusIcons.gameObject.SetActive(false);
    }

    /// <summary>
    /// I think the Function name is straightforward
    /// </summary>
    public void ActivateUIElementsAccordingToGameStatus()
    {
        if (tempStatus != GameManager.instance.GetStatus())

            DisableUIElements();

        NavigatationCamera navigationC = NavigatationCamera.instance;

        switch (GameManager.instance.GetStatus())
        {
            case GameManager.GameStatus.Table:
                navigationC.ActivateMapCamera(navigationC.GetTableCamera());
                break;

            case GameManager.GameStatus.Map:
                navigationC.ActivateMapCamera(navigationC.GetMapCamera());
                break;

            case GameManager.GameStatus.Diorama:
                statusIcons.gameObject.SetActive(true);
                navigationC.ActivateMapCamera(navigationC.GetDioramaCamera());
                break;

            case GameManager.GameStatus.Newspapaer:
                navigationC.ActivateMapCamera(navigationC.GetNewspaperCamera());
                break;

            default:
                navigationC.ActivateMapCamera(navigationC.GetTableCamera());
                break;
        }

        tempStatus = GameManager.instance.GetStatus();
    }
}
