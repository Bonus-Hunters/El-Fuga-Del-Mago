using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("General Info")]
    public string itemName;
    [TextArea] public string description;
    public Sprite icon;
    public int maxStack = 1;
    public int ID;
}
