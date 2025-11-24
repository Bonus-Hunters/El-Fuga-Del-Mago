using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;
    protected override void Start()
    {
        base.Start();
    }
    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        AssignSlots(invToDisplay);
    }

    public override void AssignSlots(InventorySystem invToDisplay)
    {
        ClearSlots();
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();
        if(inventorySystem == null) return;
        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab,transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            //can use object pooling which is better than destorying them
            Destroy(item.gameObject);
        }
        if(slotDictionary != null) slotDictionary.Clear();
    }
}
