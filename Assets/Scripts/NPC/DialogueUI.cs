/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
    public Transform optionsContainer;
    public GameObject optionButtonPrefab;
    public GameObject interactPrompt;
    public Button continueButton;

    private ConversationGraph currentConversation;
    private List<Button> currentOptionButtons = new List<Button>();

    // Singleton pattern
    private static DialogueUI instance;
    public static DialogueUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueUI>();
            }
            return instance;
        }
    }

    void Start()
    {
        HideDialogue();
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        // Setup continue button
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
            continueButton.gameObject.SetActive(false);
        }
    }

    public void ShowInteractPrompt(bool show)
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(show);
    }

    public void StartDialogue(ConversationGraph conversation, string npcName)
    {
        currentConversation = conversation;

        // Setup event listeners
        currentConversation.OnDialogueTextChanged += OnDialogueTextUpdated;
        currentConversation.OnOptionsChanged += OnOptionsUpdated;
        currentConversation.OnConversationStateChanged += OnConversationStateChanged;

        // Set NPC name
        if (npcNameText != null)
            npcNameText.text = npcName;

        ShowDialogue();
        ShowInteractPrompt(false);

        // Trigger initial UI update
        OnDialogueTextUpdated(currentConversation.GetCurrentDialogueText());
        OnOptionsUpdated(currentConversation.GetAvailableOptions());
    }

    private void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);

        // Clean up event listeners
        if (currentConversation != null)
        {
            currentConversation.OnDialogueTextChanged -= OnDialogueTextUpdated;
            currentConversation.OnOptionsChanged -= OnOptionsUpdated;
            currentConversation.OnConversationStateChanged -= OnConversationStateChanged;
        }

        ClearOptionButtons();
        HideContinueButton();
    }

    private void OnDialogueTextUpdated(string newText)
    {
        if (dialogueText != null)
            dialogueText.text = newText;
    }

    private void OnOptionsUpdated(List<DialogueOption> options)
    {
        ClearOptionButtons();

        if (options == null || options.Count == 0)
        {
            // If no options and it's an end node, show continue button
            if (currentConversation != null && currentConversation.IsCurrentNodeEnd())
            {
                ShowContinueButton();
            }
            return;
        }

        // Create buttons for each available option
        foreach (var option in options)
        {
            CreateOptionButton(option);
        }
    }

    private void CreateOptionButton(DialogueOption option)
    {
        if (optionButtonPrefab == null || optionsContainer == null) return;

        GameObject buttonObj = Instantiate(optionButtonPrefab, optionsContainer);
        Button button = buttonObj.GetComponent<Button>();
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null)
            buttonText.text = option.optionText;

        // Add click listener
        button.onClick.AddListener(() => OnOptionSelected(option));

        currentOptionButtons.Add(button);

        // Visual feedback for locked options
        if (option.requiresQuestCompletion && !QuestManager.IsQuestCompleted(option.requiredQuestID))
        {
            button.interactable = false;
            if (buttonText != null)
                buttonText.text = $"[LOCKED] {option.optionText}";
        }
    }

    private void OnOptionSelected(DialogueOption option)
    {
        if (currentConversation != null)
        {
            currentConversation.SelectOptionByTarget(option.targetNodeID);
        }
    }

    private void OnContinueClicked()
    {
        if (currentConversation != null)
        {
            currentConversation.EndConversation();
        }
    }

    private void OnConversationStateChanged(bool isActive)
    {
        if (!isActive)
        {
            HideDialogue();
        }
    }

    public void ShowContinueButton()
    {
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    public void HideContinueButton()
    {
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    private void ClearOptionButtons()
    {
        foreach (Button button in currentOptionButtons)
        {
            if (button != null)
                Destroy(button.gameObject);
        }
        currentOptionButtons.Clear();
    }

    public void UpdateDialogueDisplay()
    {
        // This method can be called externally if needed
        if (currentConversation != null)
        {
            OnDialogueTextUpdated(currentConversation.GetCurrentDialogueText());
            OnOptionsUpdated(currentConversation.GetAvailableOptions());
        }
    }
}*/