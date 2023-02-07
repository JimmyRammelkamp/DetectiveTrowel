using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private RectTransform statusIcons;
    [SerializeField] private RectTransform nPCInteractiveButtons;
    [SerializeField] private RectTransform mapButton;
    [SerializeField] private RectTransform cigUI;

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

    private void Initalize()
    {
        statusIcons.gameObject.SetActive(false);
        nPCInteractiveButtons.gameObject.SetActive(false);
        cigUI.gameObject.SetActive(false);
    }

    /// <summary>
    ///  Disable every UI elements
    /// </summary>
    private void DisableUIElements()
    {
        statusIcons.gameObject.SetActive(false);
        nPCInteractiveButtons.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(false);
    }

    public void ActicateNPCInteractiveButtons(bool _bool)
    {
        if (_bool)
        {
            nPCInteractiveButtons.position = Camera.main.WorldToScreenPoint(GameManager.instance.GetSelectedNPC().transform.position);
            nPCInteractiveButtons.position += new Vector3(Screen.height / 8, 0, 0);

            nPCInteractiveButtons.gameObject.SetActive(true);
        }
        else
        {
            nPCInteractiveButtons.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// I think the Function name is straightforward
    /// </summary>
    public void ActivateUIElementsAccordingToGameStatus(GameManager.GameStatus _status)
    {
        if (_status != tempStatus)

            DisableUIElements();

        NavigatationCamera navigationC = NavigatationCamera.instance;

        switch (_status)
        {
            case GameManager.GameStatus.Table:
                mapButton.gameObject.SetActive(true);
                navigationC.ActivateMapCamera(navigationC.GetTableCamera());
                break;

            case GameManager.GameStatus.Map:
                mapButton.gameObject.SetActive(true);
                navigationC.ActivateMapCamera(navigationC.GetMapCamera());
                break;

            case GameManager.GameStatus.Diorama:
                mapButton.gameObject.SetActive(true);
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

    /// <summary>
    /// Speak interaction with NPC
    /// </summary>
    public void Speak()
    {
        if (!GameManager.instance.GetSelectedNPC()) return;

        GameManager.instance.GetSelectedNPC().Speak();
    }

    /// <summary>
    /// Inspect interaction with NPC
    /// </summary>
    public void Inspect()
    {
        if (!GameManager.instance.GetSelectedNPC()) return;

        GameManager.instance.GetSelectedNPC().Inspect();
    }

    /// <summary>
    /// ShowObject interaction with NPC
    /// </summary>
    public void ShowObject()
    {
        if (!GameManager.instance.GetSelectedNPC()) return;

        GameManager.instance.GetSelectedNPC().ShowObject();
    }

    /// <summary>
    /// Cigaretta UI
    /// </summary>
    public void ShowAndSetCigUI(int _stressLeft, Vector3 _position)
    {
        cigUI.gameObject.SetActive(true);
        cigUI.GetChild(0).GetComponent<TextMeshProUGUI>().text = _stressLeft.ToString();
        cigUI.position = Camera.main.WorldToScreenPoint(_position);
    }

    public void DisableCigUI()
    {
        cigUI.gameObject.SetActive(false);
    }
}
