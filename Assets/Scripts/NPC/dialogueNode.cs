using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string nodeID;
    [TextArea(3, 5)]
    public string dialogueText;
    public List<DialogueOption> options;
    public bool isEndNode = false;
}

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public string targetNodeID; 
    public bool requiresQuestCompletion = false;
    public string requiredQuestID;
}