using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IObjectInteraction, IOutline
{
    [SerializeField] private RectTransform statusIcons;

    public enum InteractStatus // NPC status based on if the player already interacted with it or it has new dialogue active
    {
        Unkown = 0,
        Interacted = 1,
        NewDialogue = 2
    }

    private InteractStatus characterStatus;

    void Start()
    {
        ChangeStatus(InteractStatus.Unkown);
        if (statusIcons) statusIcons = Instantiate(statusIcons, HUDManager.instance.GetStatusIconParent());
    }

    private void OnEnable() // Shows the NPC icon when the NPC object is active
    {
        statusIcons.gameObject.SetActive(true);
    }

    private void OnDisable() // Hides the NPC icon when the NPC object is active
    {
        statusIcons.gameObject.SetActive(false);
    }

    private void Update()
    {
        IconPositionUpdate();
    }

    /// <summary>
    /// Positions the NPC icons according to the NPC position (+ shifting so doesn't cover the NPC object itself)
    /// </summary>
    private void IconPositionUpdate()
    {
        statusIcons.position = Camera.main.WorldToScreenPoint(transform.position);
        statusIcons.position += new Vector3(0, Screen.height / 10, 0);
    }

    /// <summary>
    /// Changes the NPC status and activates the right Icon
    /// </summary>
    public void ChangeStatus(InteractStatus _status)
    {
        characterStatus = _status;

        DisableAllIcons();

        statusIcons.GetChild((int)_status).gameObject.SetActive(true);
    }

    /// <summary>
    /// Disable all icons
    /// </summary>
    private void DisableAllIcons()
    {
        for (int i = 0; i < statusIcons.childCount; i++)
        {
            statusIcons.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Instert here any code to trigger when the player clicks on the NPC
    /// </summary>
    public void Interact()
    {
        if (GameManager.instance.GetStatus() != GameManager.GameStatus.Diorama) return;

        // Do stuff when you click on the NPC

        ChangeStatus(InteractStatus.Interacted);

        Debug.Log(transform.name);
    }

    public void ToggleOutline()
    {
        if (GameManager.instance.GetStatus() != GameManager.GameStatus.Diorama) return;

        if (transform.TryGetComponent(out Outline _outline))
        {
            _outline.enabled = true;
            _outline.SetIsMouseOn(true);
            _outline.SetOutlineWidth(GlobalOutlineManager.instance.GetOutlineWIdth());
        }
    }
}
