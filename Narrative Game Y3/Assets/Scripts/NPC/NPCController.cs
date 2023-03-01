using NarrativeGame.Dialogue;
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
        [SerializeField] string conversantName;
        [SerializeField] PlayCardsSObject[] questSlot = new PlayCardsSObject[3];
        PlayerConversant playerConversant;

        private PlayCardsSObject[] playingCardsOnSlot = new PlayCardsSObject[3];

        [SerializeField] private bool isQuestAvaliable = false;

        public PlayCardsSObject[] GetPlayingCardsOnSlot() { return playingCardsOnSlot; }

        public void SetIsQuestAbaliable(bool _bool) { isQuestAvaliable = true; }

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
        }

        void Awake()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
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
            if (isQuestAvaliable) HUDManager.instance.SetCallButton(true);
        }

        public string GetName()
        {
            return conversantName;
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
            GameManager.instance.SetStatus(GameManager.GameStatus.InspectEvidence);
            PlayingCardManager.instance.InteractWithDeck(CardType.All);
        }

        public void Call()
        {
            Debug.Log("Call to end the Quest");
            GameManager.instance.SetStatus(GameManager.GameStatus.Call);
            PlayingCardManager.instance.InteractWithDeck(CardType.All);
            Telephone.instance.PickUpPhone();
        }

        public void FinalConfirm()
        {
            Debug.Log("Final Confirm");
            Telephone.instance.PutDownpPhone();

            int counter = 0;

            for (int i = 0; i < questSlot.Length; i++)
            {
                for (int j = 0; j < playingCardsOnSlot.Length; j++)
                {
                    if (questSlot[i] == playingCardsOnSlot[j]) counter++;
                }
            }

            if (counter < 3) QUestFailed();
            else QuestCompleted();
        }

        public void InspectEvidence(PlayCardsSObject _card)
        {
            Debug.Log("Inspect this: " + _card + " -" + transform.name);
        }

        private void QuestCompleted()
        {
            Debug.Log("Quest Completed");
            GameManager.instance.SetStatus(GameManager.GameStatus.Diorama);
            isQuestAvaliable = false;
        }

        private void QUestFailed()
        {
            Debug.Log("Quest Failed");
            GameManager.instance.SetStressLevel(GameManager.instance.GetStressLevel() - 1);
            GameManager.instance.SetStatus(GameManager.GameStatus.Table);
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

        public void AddCardToNPCSlot(Transform _card)
        {
            int typeIndex = (int)_card.GetComponent<PlayingCard>().GetCardData().Type - 1;
            playingCardsOnSlot[typeIndex] = _card.GetComponent<PlayingCard>().GetCardData();

            CheckSlot();
        }

        public void RemoveCardFromNPCSlot(int _index)
        {
            _index--;

            if (playingCardsOnSlot[_index] == null) return;

            playingCardsOnSlot[_index] = null;

            CheckSlot();
        }

        private void CheckSlot()
        {
            for (int i = 0; i < playingCardsOnSlot.Length; i++)
            {
                if (playingCardsOnSlot[i] == null)
                {
                    HUDManager.instance.ShowFinalButton(false);
                    return;
                }
            }
            HUDManager.instance.ShowFinalButton(true);
        }

    }
}
