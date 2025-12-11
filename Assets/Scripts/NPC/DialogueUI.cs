using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public TMP_Text option0;
    public TMP_Text option1;
    public TMP_Text option2;
    public TMP_Text option3;

    private List<TMP_Text> optionSlots;
    private DialogueNode currentNode;

    void Awake()
    {
        optionSlots = new List<TMP_Text> { option0, option1, option2, option3 };
    }

    public void ShowNode(DialogueNode node)
    {
        dialoguePanel.SetActive(true);
        currentNode = node;

        dialogueText.text = node.dialogueText;

        for (int i = 0; i < optionSlots.Count; i++)
        {
            if (i < node.options.Count)
            {
                optionSlots[i].text = node.options[i].optionText;
                optionSlots[i].gameObject.SetActive(true);
            }
            else
            {
                optionSlots[i].gameObject.SetActive(false);
            }
        }
    }
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
