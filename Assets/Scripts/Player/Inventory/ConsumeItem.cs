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
        if (Input.inputString.Length > 0)
        {
            char c = Input.inputString[0];

            // Check if it's a number 1–9
            if (char.IsDigit(c))
            {
                int ind = c - '0';
                if (ind >= 1 && ind <= 9)
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
                                player.RestoreHealth(healAmount);
                                slot.RemoveFromStack(1);
                                inventorySystem.onInventorySlotChanged?.Invoke(slot);
                                return;
                            case 1:
                                Debug.Log("Consuming Mana Potion.");
                                player.RestoreMana(manaAmount);
                                slot.RemoveFromStack(1);
                                inventorySystem.onInventorySlotChanged?.Invoke(slot);

                                return;
                            case 2:
                                Debug.Log("Consuming Speed Potion.");
                                return;
                        }
                    }
                    Debug.Log("Pressed number: " + ind);
                }
            }
        }
    }

}
