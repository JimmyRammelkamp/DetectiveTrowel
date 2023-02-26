using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NarrativeGame.Dialogue;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameStatus
    {
        None = 0,
        Table = 1,
        Map = 2,
        Diorama = 3,
        Newspaper = 4,
        PlayingCard = 5
    }

    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private int stressLevel;

    private InputActions input;

    private NPCController selectedNPC;

    private LayerMask rayLayer;

    public InputActions GetInputs() { return input; }
    public int GetStressLevel() { return stressLevel; }
    public void SetStressLevel(int _stressLevel) { stressLevel = _stressLevel; }
    public NPCController GetSelectedNPC() { return selectedNPC; }
    public void SetNPC(NPCController _npc) { selectedNPC = _npc; }
    public GameStatus GetStatus() { return gameStatus; }
    public void SetStatus(GameStatus _status) 
    { 
        gameStatus = _status;
        HUDManager.instance.ActivateUIElementsAccordingToGameStatus(gameStatus);
    }

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (GameManager)");
        instance = this;
    }

    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        MouseCheck();
    }

    private void Initialize()
    {
        SetStatus(GameStatus.Table);

        input = new InputActions();
        input.GameInput.Enable();

        input.GameInput.Map.performed += MapToggle;
        input.GameInput.LMB.performed += InteractWith;
        input.GameInput.Back.performed += Back;

        input.GameInput.ScrollWheel.performed += ScrollNavigation;
        input.GameInput.ScrollWheel.performed += ScrollNavigation;
    }

    /// <summary>
    /// Interaction with the mouse scrollwheel
    /// </summary>
    private void ScrollNavigation(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().y < 0) Back(context);
        if (context.ReadValue<Vector2>().y > 0) InteractWith(context);
    }

    /// <summary>
    /// Back from the current GameMode to the table view
    /// </summary>
    private void Back(InputAction.CallbackContext context)
    {
        if (!ReadyToContinue()) return;

        SetStatus(GameStatus.Table);

        DisableNPCElements();
    }

    /// <summary>
    /// Toggle between Table and Map camera (trigger part with pressing "M")
    /// </summary>
    private void MapToggle(InputAction.CallbackContext context)
    {
        MapToggle();
    }

    public void MapToggle()
    {
        if (!ReadyToContinue()) return;

        DisableNPCElements();

        NavigationCamera.instance.ToggleMap();
    }

    // Checks if Pointer is on a certain UI elements (Button here)
    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = input.GameInput.MousePosition.ReadValue<Vector2>();
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);

        for (int index = 0; index < raysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = raysastResults[index];

            if (curRaysastResult.gameObject.transform.TryGetComponent(out Button _UIElement))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Interact with the object if it contains the IObjectInteraction interface
    /// Using layers at the moment, it might change (or not)
    /// </summary>
    private void InteractWith(InputAction.CallbackContext context)
    {
        if (!ReadyToContinue()) return;

        Ray ray = Camera.main.ScreenPointToRay(input.GameInput.MousePosition.ReadValue<Vector2>());

        if (selectedNPC && !IsPointerOverUIElement())
        {
            DisableNPCElements();
        }

        if (IsPointerOverUIElement()) return;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (var item in hits)
        {
            if (item.transform.TryGetComponent(out InteractableObjects interactable))
            {
                if (interactable.isObjectInteractivable()) interactable.Interact();
            }
        }
    }

    /// <summary>
    /// Toggles the outline when the mouse is on any interactivable object
    /// </summary>
    private void MouseCheck()
    {
        if (!ReadyToContinue()) return;
        if (IsPointerOverUIElement()) return;

        Ray ray = Camera.main.ScreenPointToRay(input.GameInput.MousePosition.ReadValue<Vector2>());

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (var item in hits)
        {
            if (item.transform.TryGetComponent(out InteractableObjects interactable))
            {
                if (interactable.isObjectInteractivable()) interactable.MouseEnter();
            }
        }
    }

    private void DisableNPCElements()
    {
        if (!selectedNPC) return;

        selectedNPC.GetComponent<Outline>().SetToggle(false);
        HUDManager.instance.ActivateNPCInteractiveButtons(false);
        selectedNPC = null; 
    }

    /// <summary>
    ///  Check is the Camera transition or the diorama animation is over or not
    /// </summary>
    public bool ReadyToContinue()
    {
        if (NavigationCamera.instance.IsCameraTransitionOver()) return false;
        if (DioramaManager.instance.IsDioramaAnimationOver()) return false;

        return true;
    }

    public void ToggleOutline(GameObject _object, bool _toggle)
    {
        if (_toggle)
        {
            if (_object.TryGetComponent(out Outline _childOutline))
            {
                _childOutline.enabled = true;
                _childOutline.SetToggle(true);
                _childOutline.SetOutlineWidth(GlobalOutlineManager.instance.GetOutlineWIdth());
            }
        }
        else
        {
            if (_object.TryGetComponent(out Outline _childOutline))
            {
                _childOutline.enabled = false;
                _childOutline.SetToggle(false);
            }
        }
    }
}
