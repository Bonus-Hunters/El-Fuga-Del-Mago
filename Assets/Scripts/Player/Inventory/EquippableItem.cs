using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    [CreateAssetMenu(fileName = "NewEquippableItem", menuName = "Inventory/Equippable")]
    public class EquippableItem : InventoryItem
    {
        [Header("Equip Settings")]
        public GameObject prefab;          // Prefab for the weapon (FPS hands/gun)
        public Transform equipSlot;        // Where to attach prefab
    }
}
