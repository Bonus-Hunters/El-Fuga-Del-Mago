using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationGraph : MonoBehaviour
{
    [Header("Conversation Data")]
    public string conversationID;
    public List<DialogueNode> nodes;
    public string startNodeID;

    [Header("Quest Requirements")]
    public bool requiresQuestCompletion = false;
    public string requiredQuestID;

    private Dictionary<string, DialogueNode> nodeDictionary;
    private DialogueNode currentNode;
    private bool conversationActive = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeNodeDictionary();
    }

    private void InitializeNodeDictionary()
    {
        nodeDictionary = new Dictionary<string, DialogueNode>();
        foreach (var node in nodes)
        {
            nodeDictionary[node.nodeID] = node;
        }
    }

    public bool CanStartConversation()
    {
        if (requiresQuestCompletion)
        {
            return QuestManager.IsQuestCompleted(requiredQuestID);
        }
        return true;
    }

    public void StartConversation()
    {
        if (!CanStartConversation())
        {
            Debug.Log($"Cannot start conversation {conversationID}. Quest {requiredQuestID} not completed.");
            return;
        }

        if (nodeDictionary.ContainsKey(startNodeID))
        {
            conversationActive = true;
            currentNode = nodeDictionary[startNodeID];
            DisplayCurrentNode();
        }
    }

    private void DisplayCurrentNode()
    {
        if (currentNode == null) return;

        Debug.Log($"NPC: {currentNode.dialogueText}");

        if (currentNode.isEndNode)
        {
            EndConversation();
            return;
        }

        // Display available options
        for (int i = 0; i < currentNode.options.Count; i++)
        {
            var option = currentNode.options[i];

            // Check if option is available (quest requirements)
            if (IsOptionAvailable(option))
            {
                Debug.Log($"{i + 1}. {option.optionText}");
            }
            else
            {
                Debug.Log($"{i + 1}. [LOCKED] {option.optionText}");
            }
        }
    }

    private bool IsOptionAvailable(DialogueOption option)
    {
        if (option.requiresQuestCompletion)
        {
            return QuestManager.IsQuestCompleted(option.requiredQuestID);
        }
        return true;
    }

    public void SelectOption(int optionIndex)
    {
        if (!conversationActive || currentNode == null) return;

        if (optionIndex >= 0 && optionIndex < currentNode.options.Count)
        {
            var selectedOption = currentNode.options[optionIndex];

            // Check if option is available
            if (!IsOptionAvailable(selectedOption))
            {
                Debug.Log("This option is not available yet!");
                return;
            }

            // Move to next node
            if (nodeDictionary.ContainsKey(selectedOption.targetNodeID))
            {
                currentNode = nodeDictionary[selectedOption.targetNodeID];
                DisplayCurrentNode();
            }
            else
            {
                Debug.LogError($"Target node {selectedOption.targetNodeID} not found!");
                EndConversation();
            }
        }
    }

    private void EndConversation()
    {
        conversationActive = false;
        currentNode = null;
        Debug.Log("Conversation ended.");
    }

    // Update is called once per frame
    void Update()
    {
        // Example input handling - you can replace this with your UI system
        if (conversationActive)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    SelectOption(i);
            }
        }
    }
}