using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsystem : MonoBehaviour
{
    [Header("NPC Settings")]
    public string npcName;

    [Header("Conversation Reference")]
    public ConversationGraph conversation;

    private bool player_detected = false;

    // Update is called once per frame
    void Update()
    {
        if (player_detected && Input.GetKeyDown(KeyCode.E))
        {
            if (conversation != null)
            {
                conversation.StartConversation();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detected = true;
            Debug.Log("Player detected! Press E to talk.");

            // Show UI indicator
         //   UIManager.ShowInteractPrompt("Press E to talk");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detected = false;
            Debug.Log("Player left the area!");

            // Hide UI indicator
        //    UIManager.HideInteractPrompt();
        }
    }
}