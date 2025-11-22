using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour, IInteractable
{
    public InventoryItem Item;
    public void Interact(GameObject interactor)
    {
        var inventory = interactor.GetComponent<InventoryHolder>();
        if (!inventory) return;
        if (inventory.InventorySystem.AddToInventory(Item, 1))
        {
            Destroy(gameObject);
        }
    }

    public string InterationPrompt()
    {
        return "Press E to pick up";
    }
}
