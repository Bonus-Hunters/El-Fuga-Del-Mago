using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItem itemData;
    [SerializeField] private int stackSize;
    public InventoryItem ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot(InventoryItem itemData, int amount)
    {
        this.itemData = itemData;
        this.stackSize = amount;
    }
    public InventorySlot()
    {
        ClearSlot();
    }
    public void ClearSlot()
    {
        this.itemData = null;
        this.stackSize = 0;
    }
    public void UpdateInventorySlot( InventoryItem data , int amout)
    {
        itemData = data;
        stackSize = amout;
    }
    public bool RoomLeftInStack(int amountToAdd,out int amountRemaining)
    {
        amountRemaining = ItemData.maxStack - stackSize;
        return RoomLeftInStack(amountToAdd);
    }
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= ItemData.maxStack)
        {
            return true;
        }
        return false;
    }
    public void AddToStack(int amount)
    {
        stackSize += amount;
    }
    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
