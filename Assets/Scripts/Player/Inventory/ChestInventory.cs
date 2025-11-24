using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ChestInventory : InventoryHolder, IInteractable
{
    private UnityAction<IInteractable> OnInteractionComplete { get;  set; }

    public void Interact(GameObject interactor)
    {
        onDynamicInventoryDisplayRequested?.Invoke(inventorySystem);
        //UIStateController.Instance.EnterUI();
    }

    public string InterationPrompt()
    {
        return "Press E to open Chest";
    }
}
