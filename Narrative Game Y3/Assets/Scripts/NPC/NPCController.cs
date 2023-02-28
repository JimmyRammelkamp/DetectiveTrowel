using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NarrativeGame.Dialogue
{
    public class NPCController : MonoBehaviour, IObjectInteraction
    {
        [SerializeField] private RectTransform statusIcons;
        [SerializeField] Dialogue dialogue = null;
        string conversantName = "???";
        PlayerConversant playerConversant;

        public enum InteractStatus // NPC status based on if the player already interacted with it or it has new dialogue active
        {
            Unknown = 0,
            Interacted = 1,
            NewDialogue = 2
        }

        private InteractStatus characterStatus;

        void Start()
        {
            ChangeStatus(InteractStatus.Unknown);
            if (statusIcons) statusIcons = Instantiate(statusIcons, HUDManager.instance.GetStatusIconParent());
            playerConversant = FindObjectOfType<PlayerConversant>();
        }

        private void OnEnable() // Shows the NPC icon when the NPC object is active
        {
            statusIcons.gameObject.SetActive(true);
        }

        private void OnDisable() // Hides the NPC icon when the NPC object is active
        {
            if (statusIcons) statusIcons.gameObject.SetActive(false);
        }

        private void Update()
        {
            IconPositionUpdate();
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
        /// Insert here any code to trigger when the player clicks on the NPC
        /// </summary>
        public void Interact()
        {
            if (GameManager.instance.GetStatus() != GameManager.GameStatus.Diorama) return;

            GameManager.instance.SetNPC(this);

            if (transform.TryGetComponent(out Outline _outline))
            {
                _outline.enabled = true;
                _outline.SetToggle(true);
                _outline.SetOutlineWidth(GlobalOutlineManager.instance.GetOutlineWIdth());
            }

            HUDManager.instance.ActivateNPCInteractiveButtons(true);
        }

        public string GetName()
        {
            return conversantName;
        }

        public void SetName(string name)
        {
            conversantName = name;
        }

        public void Speak()
        {
            Debug.Log("NPC Speak");

            playerConversant.StartDialogue(this, dialogue);

            ChangeStatus(InteractStatus.Interacted);
        }

        public void Inspect()
        {
            Debug.Log("NPC Inspect");
        }

        public void ShowObject()
        {
            Debug.Log("NPC Show Object");
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
    }
}
