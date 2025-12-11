using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsystem : MonoBehaviour , IInteractable
{
    [Header("NPC Settings")]
    public string npcName;

    [Header("Conversation Reference")]
    public ConversationGraph conversation;

    public string InterationPrompt()
    {
        return "Press E to talk to " + npcName;
    }

    public void Interact(GameObject interactor)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (conversation != null)
            {
                conversation.StartConversation();
            }
        }
    }
}