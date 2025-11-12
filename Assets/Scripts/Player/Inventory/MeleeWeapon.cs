using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    [System.Serializable]
    public class MeleeWeapon : Weapon
    {
        public MeleeWeapon(EquippableItem data) : base(data) { }
        public override void Attack()
        {
            Debug.Log("Swinging " + DataItem.name + " for " + damage + " damage!");
        }
    }
}
