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
        public MeleeHitbox hitbox;

        void Start()
        {
                hitbox = gameObject.AddComponent<MeleeHitbox>();
                hitbox.Setup(weapon);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.Attack();
            }
        }
    }
}
