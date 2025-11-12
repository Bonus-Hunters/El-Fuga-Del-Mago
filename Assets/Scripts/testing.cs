using Assets.Scripts.Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class testing : MonoBehaviour
    {
        void Start()
        {
            // Create a ScriptableObject instance in code (for testing)
            EquippableItem swordData = ScriptableObject.CreateInstance<EquippableItem>();
            swordData.name = "Test Sword";

            // Create a MeleeWeapon instance using the ScriptableObject
            MeleeWeapon sword = new MeleeWeapon(swordData);

            // Set some runtime values
            sword.attackTime = 0f;
            sword.damage = 10f;
            sword.range = 2f;
            sword.attackSpeed = 1.5f;
            sword.attackTime = 0f;
            sword.cooldownTime = 1f;

            // Test: Print info
            Debug.Log("Weapon Name: " + sword.DataItem.name);
            Debug.Log("Weapon Damage: " + sword.damage);
            Debug.Log("Weapon Range: " + sword.range);

            // Call attack
            sword.Attack();
        }
    }
}
