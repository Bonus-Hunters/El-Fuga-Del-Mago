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
        public MeleeWeapon weapon;



        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.Attack();
            }
        }
    }
}
