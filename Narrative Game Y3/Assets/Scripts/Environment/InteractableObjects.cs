using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour, IObjectInteraction, IOutline
{
    [SerializeField] private GameManager.GameStatus thisObjectStatus;

    /// <summary>
    ///  Changes the GameStatus after the player interact with something
    /// </summary>
    public void Interact()
    {
        if (GameManager.instance.GetStatus() != GameManager.GameStatus.Table) return;

        // Do stuff when you click on the object

        GameManager.instance.SetStatus(thisObjectStatus);

        Debug.Log(transform.name);
    }

    public void ToggleOutline()
    {
        if (GameManager.instance.GetStatus() != GameManager.GameStatus.Table) return;

        if (transform.TryGetComponent(out Outline _outline))
        {
            _outline.enabled = true;
            _outline.SetIsMouseOn(true);
            _outline.SetOutlineWidth(GlobalOutlineManager.instance.GetOutlineWIdth());
        }
    }
}
