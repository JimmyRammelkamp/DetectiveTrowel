using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private InputActions input;

    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (GameManager)");
        instance = this;
    }

    void Start()
    {
        input = new InputActions();
        input.GameInput.Enable();
    }

    void Update()
    {
        MouseCheck();
    }

    private void MouseCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(input.GameInput.Mouse.ReadValue<Vector2>());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent(out Outline _outline))
            {
                //_outline.SetIsMouseOn(true);
                //_outline.SetOutlineWidth(GlobalOutlineManager.instance.GetOutlineWIdth());
            }
        }
    }
}
