using NarrativeGame.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeGame.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName;

        public CursorType GetCursorType()
        {
            return CursorType.DialogueHover;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(dialogue == null) 
            {
                return false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}