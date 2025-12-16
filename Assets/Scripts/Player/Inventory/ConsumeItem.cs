using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ConsumeItems : MonoBehaviour
{
    // has Slots -> each slot has  InventorySlot_UI
    public InventorySystem inventorySystem;
    private Player player;
    private float healAmount = 20f;
    private float manaAmount = 20f;
    /*
        ID: Potion Name 
        0: Health Potion
        1: Mana Potion
        2: Speed Potion   
    */
    void Start()
    {
        player = GetComponent<Player>();
        var inventory = GetComponent<InventoryHolder>();
        if (!inventory) return;
        inventorySystem = inventory.InventorySystem;
    }
    void Update()
    {
        if (Input.inputString.Length > 0 && Player.playerInUI==false)
        {
            char c = Input.inputString[0];

            // Check if it's a number 1–5
            if (char.IsDigit(c))
            {
                int ind = c - '0';
                if (ind >= 1 && ind <= 5)
                {
                    ind--;
                    if (inventorySystem.InventorySlots[ind] != null)
                    {
                        InventoryItem itemInSlot = inventorySystem.InventorySlots[ind].ItemData;
                        InventorySlot slot = inventorySystem.InventorySlots[ind];

                        if (itemInSlot == null) return;
                        switch (itemInSlot.ID)
                        {
                            case 0:
                                Debug.Log("Consuming Health Potion.");
                                bool didHeal = player.RestoreHealth(healAmount);
                                if (didHeal)
                                {
                                    slot.RemoveFromStack(1);

                                    inventorySystem.onInventorySlotChanged?.Invoke(slot);
                                }
                                break;
                            case 1:
                                Debug.Log("Consuming Mana Potion.");
                                bool didRecharge = player.RestoreMana(manaAmount);
                                if (didRecharge)
                                {
                                    slot.RemoveFromStack(1);
                                    inventorySystem.onInventorySlotChanged?.Invoke(slot);                                    
                                }
                                break;
                            case 2:
                                Debug.Log("Consuming Speed Potion.");
                                break;
                        }
                    }
                    Debug.Log("Pressed number: " + ind);
                }
            }
        }
    }

}
