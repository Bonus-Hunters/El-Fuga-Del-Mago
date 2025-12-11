using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour, IInteractable
{
    [Header("NPC Settings")]
    public string npcName;

    [Header("Conversation Reference")]
    public ConversationGraph conversation;

    [Header("Enemies to activate")]
    public List<GameObject> Enemies = new List<GameObject>();

    [Header("object to activate before quest")]
    public List<GameObject> before_objects = new List<GameObject>();

    [Header("object to activate after quest")]
    public List<GameObject> after_objects = new List<GameObject>();

    private bool isCompleted = false;
    private bool questStart = false;

    public string InterationPrompt()

    {
        if (isCompleted)
            return npcName + ": Have a nice day,mr sherlock ";
        else if (conversation.conversationActive)
            return "";
        else
            return "Press E to talk to " + npcName;
    }
    private void startQuest()
    {
        questStart = true;
        foreach (var enemy in Enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
        foreach (var obj in before_objects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
    void Update()
    {
        if (conversation != null && !isCompleted && conversation.isDone() && !questStart)
            startQuest();
        if (!questStart || isCompleted)
            return;

        bool x = false;
        foreach (var enemy in Enemies)
        {
            if (enemy != null)
            {
                x = true;
            }
        }
        Debug.Log("ya rb");
        if (!x)
        {
            Debug.Log("mission complete");
            isCompleted = true;
            QuestManager.CompleteQuest("inventory");
            foreach (var obj in after_objects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
    public void Interact(GameObject interactor)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (conversation != null && !isCompleted)
            {
                conversation.StartConversation();

            }
        }
    }

}