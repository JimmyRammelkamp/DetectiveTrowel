using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField] private GameManager.GameStatus changeStatusToThis;
    [SerializeField] private List<GameManager.GameStatus> thisObjectSAlaviable;

    private bool isMouseEnter = false;
    private bool doubleCheck = false;

    public bool isObjectInteractivable()
    {
        bool isAvaliable = false;

        foreach (var status in thisObjectSAlaviable)
        {
            if (GameManager.instance.GetStatus() == status) isAvaliable = true;
        }

        return isAvaliable;
    }

    private void Update()
    {
        MouseExit();
    }

    public void Interact()
    {
        if (changeStatusToThis != GameManager.GameStatus.None) GameManager.instance.SetStatus(changeStatusToThis);

        if (transform.TryGetComponent(out IObjectInteraction _target))
        {
            _target.Interact();
        }
    }

    public void MouseEnter()
    {
        doubleCheck = true;
        if (isMouseEnter) return;
        isMouseEnter = true;

        if (transform.TryGetComponent(out IMouseEnter _target))
        {
            _target.MouseEnter();
        }

        GameManager.instance.ToggleOutline(gameObject, true);
    }


    public void MouseExit()
    {
        if (doubleCheck)
        {
            doubleCheck = false;
            return;
        }


        if (isMouseEnter)
        {
            isMouseEnter = false;
            if (transform.TryGetComponent(out IMouseExit _target))
            {
                _target.MouseExit();
            }

            GameManager.instance.ToggleOutline(gameObject, false);
        }
    }
}
