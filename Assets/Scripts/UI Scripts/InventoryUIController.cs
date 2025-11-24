using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    [SerializeField] private Player player;
    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        InventoryHolder.onDynamicInventoryDisplayRequested += DisplayInventory;
    }
    private void OnDisable()
    {
        InventoryHolder.onDynamicInventoryDisplayRequested -= DisplayInventory;
    }
    private void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
        player.IsInUI = true;
    }

    void Update()
    {
        if(inventoryPanel.gameObject.activeInHierarchy &&
            Keyboard.current.qKey.wasPressedThisFrame)
        {
            inventoryPanel.gameObject.SetActive(false);
            player.IsInUI = false;
        }
    }
}
