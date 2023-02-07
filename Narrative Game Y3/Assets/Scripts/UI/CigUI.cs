using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigUI : MonoBehaviour
{
    /// <summary>
    /// Shows the stress level 
    /// </summary>
    private void OnMouseEnter()
    {
        if (!GameManager.instance.ReadyToContinue()) return;

        HUDManager.instance.ShowAndSetCigUI(GameManager.instance.GetStressLevel(), transform.position);
    }

    /// <summary>
    /// Disable it
    /// </summary>
    private void OnMouseExit()
    {
        HUDManager.instance.DisableCigUI();
    }

}
