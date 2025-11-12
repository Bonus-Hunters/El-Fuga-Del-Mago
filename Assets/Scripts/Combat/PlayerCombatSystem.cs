using Assets.Scripts.Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    internal class PlayerCombatSystem : MonoBehaviour
    {
        public Weapon equippedWeapon;
        public Transform attackOrigin;

        void Start()
        {
            if (equippedWeapon != null)
                equippedWeapon.attackOrigin = attackOrigin;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && equippedWeapon != null)
            {
                Debug.Log("Player attacking with " + equippedWeapon.name);
                equippedWeapon.Attack();
                Debug.Log("Attack finished.");
            }
        }
    }
}
