using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
[System.Serializable]
public class InventorySystem 
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;
    public UnityAction<InventorySlot> onInventorySlotChanged;
    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        for(int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }
    public bool AddToInventory(InventoryItem itemToAdd,int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // check wether iten exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    onInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }

        }
        if (HasFreeSlot(out InventorySlot freeSlot)) // gets the first available slot
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            onInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }
    public bool ContainsItem(InventoryItem itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot.Count>0;
    }
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
