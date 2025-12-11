using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestInventory : InventoryHolder, IInteractable
{
    public void Interact(GameObject interactor)
    {
        onDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
    }

    public string InterationPrompt()
    {
        return "Press E to open Chest";
    }
}
