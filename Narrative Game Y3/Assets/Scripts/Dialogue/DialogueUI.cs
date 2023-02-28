using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NarrativeGame.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace NarrativeGame.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;
        [SerializeField] TextMeshProUGUI conversantName;

        void Start()
        {
            playerConversant = FindObjectOfType<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());

            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }
            conversantName.text = playerConversant.GetCurrentConversantName();

            // Enable/Disable appropriate UI Field
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText(); // Update Textbox with current text
                nextButton.gameObject.SetActive(playerConversant.HasNext()); // Show Next Button if there is another text in sequence
                quitButton.gameObject.SetActive(!playerConversant.HasNext()); // Show Quit Button if there is no text in sequence
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot) // Destroy previous choices
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices()) // Populate current choices
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();

                Button button = choiceInstance.GetComponentInChildren<Button>();

                // lambda function creates a listener function for each button
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}