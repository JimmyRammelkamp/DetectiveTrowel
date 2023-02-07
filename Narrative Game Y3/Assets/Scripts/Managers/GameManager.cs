using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameStatus
    {
        Table = 0,
        Map = 1,
        Diorama = 2,
        Newspapaer = 3
    }

    [SerializeField] private GameStatus gameStatus;

    private InputActions input;

    public GameStatus GetStatus() { return gameStatus; }
    public void SetStatus(GameStatus _status) { gameStatus = _status; }

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (GameManager)");
        instance = this;
    }

    void Start()
    {
        Initalize();
    }

    private void Initalize()
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

    void Update()
    {
        MouseCheck();
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

        gameStatus = GameStatus.Table;
    }

    /// <summary>
    /// Toggle between Table and Map camera (trigger part with pressing "M")
    /// </summary>
    private void MapToggle(InputAction.CallbackContext context)
    {
        if (!ReadyToContinue()) return;

        NavigatationCamera.instance.ToggleMap();
    }

    /// <summary>
    /// Interact with the object if it contains the IObjectInteraction interface
    /// Using layers at the moment, it might change (or not)
    /// </summary>
    private void InteractWith(InputAction.CallbackContext context)
    {
        if (!ReadyToContinue()) return;

        LayerMask rayLayer = LayerMask.GetMask("Table");

        if (gameStatus != GameStatus.Table) rayLayer = LayerMask.GetMask("ZoomedIn");

        Ray ray = Camera.main.ScreenPointToRay(input.GameInput.MousePosition.ReadValue<Vector2>());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            if (hit.transform.TryGetComponent(out IObjectInteraction interactivable))
            {
                interactivable.Interact();
            }
        }
    }

    /// <summary>
    /// Toggles the outline when the mouse is on any interactivable object
    /// </summary>
    private void MouseCheck()
    {
        if (!ReadyToContinue()) return;

        LayerMask rayLayer = LayerMask.GetMask("Table");

        if (gameStatus != GameStatus.Table) rayLayer = LayerMask.GetMask("ZoomedIn");

        Ray ray = Camera.main.ScreenPointToRay(input.GameInput.MousePosition.ReadValue<Vector2>());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            if (hit.transform.TryGetComponent(out IOutline _outline))
            {
                _outline.ToggleOutline();
            }
        }
    }

    /// <summary>
    ///  Check is the Camera transition or the diorama animation is over or not
    /// </summary>
    public bool ReadyToContinue()
    {
        if (NavigatationCamera.instance.IsCameraTransitionOver()) return false;
        if (DioramaManager.instance.IsDioramaAnimationOver()) return false;

        return true;
    }
}
