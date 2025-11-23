using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI,InventorySlot> SlotDictionary => slotDictionary;
    protected virtual void Start()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay);
    public virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach(var slot in slotDictionary)
        {
            if(slot.Value == updatedSlot) // slot value "under the hood" inventory slot
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key the UI representation of the value
            }
        }
    }
    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }
        else if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        else if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;

            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
            }
            else if (isSameItem && 
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize,out int amountRemaining))
            {
                if(amountRemaining < 1) SwapSlots(clickedUISlot); // stack is full so swap items.
                else // slot is not full so take what is needed from mouse inventory
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - amountRemaining;
                    clickedUISlot.AssignedInventorySlot.AddToStack(amountRemaining);
                    clickedUISlot.UpdateUISlot();
                    mouseInventoryItem.AssignedInventorySlot
                        .UpdateInventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                }
            }
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
            }
        }


    }
    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData,
            mouseInventoryItem.AssignedInventorySlot.StackSize);

        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
