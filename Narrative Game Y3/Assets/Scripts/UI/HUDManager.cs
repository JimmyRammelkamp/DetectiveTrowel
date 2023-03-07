using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NarrativeGame.Dialogue;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private RectTransform statusIcons;
    [SerializeField] private RectTransform npcInteractiveButtons;
    [SerializeField] private RectTransform mapButton;
    [SerializeField] private RectTransform backButton;
    [SerializeField] private RectTransform finalButton;
    [SerializeField] private RectTransform callButton;
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
        Initialize();
    }

    private void Initialize()
    {
        statusIcons.gameObject.SetActive(false);
        npcInteractiveButtons.gameObject.SetActive(false);
        cigUI.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        finalButton.gameObject.SetActive(false);
        callButton.gameObject.SetActive(false);
    }

    /// <summary>
    ///  Disable every UI elements
    /// </summary>
    private void DisableUIElements()
    {
        statusIcons.gameObject.SetActive(false);
        npcInteractiveButtons.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        finalButton.gameObject.SetActive(false);

        PlayingCardManager.instance.CloseCardMenu();
        Telephone.instance.PutDownpPhone();
    }

    public void ActivateNPCInteractiveButtons(bool _bool)
    {
        callButton.gameObject.SetActive(false);

        if (_bool)
        {
            npcInteractiveButtons.position = Camera.main.WorldToScreenPoint(GameManager.instance.GetSelectedNPC().transform.position);
            npcInteractiveButtons.position += new Vector3(Screen.height / 8, 0, 0);

            npcInteractiveButtons.gameObject.SetActive(true);
        }
        else
        {
            npcInteractiveButtons.gameObject.SetActive(false);
        }
    }

    public void SetCallButton(bool _bool)
    {
        callButton.gameObject.SetActive(_bool);
    }

    /// <summary>
    /// I think the Function name is straightforward
    /// </summary>
    public void ActivateUIElementsAccordingToGameStatus(GameManager.GameStatus _status)
    {
        if (_status != tempStatus)

            DisableUIElements();

        NavigationCamera navigationC = NavigationCamera.instance;

        switch (_status)
        {
            case GameManager.GameStatus.Table:
                backButton.gameObject.SetActive(false);
                StartCoroutine(ShowAfterTransition(mapButton));
                navigationC.ActivateCamera(navigationC.GetTableCamera());
                break;

            case GameManager.GameStatus.Map:
                StartCoroutine(ShowAfterTransition(backButton));
                navigationC.ActivateCamera(navigationC.GetMapCamera());
                break;

            case GameManager.GameStatus.Diorama:
                StartCoroutine(ShowAfterTransition(backButton));
                StartCoroutine(ShowAfterTransition(mapButton));
                statusIcons.gameObject.SetActive(true);
                navigationC.ActivateCamera(navigationC.GetDioramaCamera());
                break;

            case GameManager.GameStatus.Newspaper:
                StartCoroutine(ShowAfterTransition(backButton));
                navigationC.ActivateCamera(navigationC.GetNewspaperCamera());
                break;

            case GameManager.GameStatus.PlayingCard:
                backButton.gameObject.SetActive(true);
                break;

            case GameManager.GameStatus.InspectEvidence:
                backButton.gameObject.SetActive(true);
                break;

            case GameManager.GameStatus.Call:
                navigationC.ActivateCamera(navigationC.GetTableCamera());
                backButton.gameObject.SetActive(true);
                break;

            case GameManager.GameStatus.Dialogue:
                navigationC.ActivateCamera(navigationC.GetDialogueCamera());
                backButton.gameObject.SetActive(false);
                break;

            default:
                navigationC.ActivateCamera(navigationC.GetTableCamera());
                break;
        }

        tempStatus = GameManager.instance.GetStatus();
    }

    IEnumerator ShowAfterTransition(RectTransform _transform)
    {
        yield return new WaitForSeconds(0.1f);
        _transform.gameObject.SetActive(false);
        yield return new WaitUntil(() => GameManager.instance.ReadyToContinue());
        _transform.gameObject.SetActive(true);
    }

    public void ShowFinalButton(bool _bool)
    {
        finalButton.gameObject.SetActive(_bool);
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
    /// Call interaction with NPC
    /// </summary>
    public void Call()
    {
        if (!GameManager.instance.GetSelectedNPC()) return;

        GameManager.instance.GetSelectedNPC().Call();
    }

    public void FinalConfirm()
    {
        if (!GameManager.instance.GetSelectedNPC()) return;

        GameManager.instance.GetSelectedNPC().FinalConfirm();
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
