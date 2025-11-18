using Assets.Scripts.Combat;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    internal class WeaponHandler : MonoBehaviour, IInteractable
    {
        private PlayerCombatSystem playerCombatSystem;
        public Weapon weaponData;
        public string InterationPrompt()
        {
            return "Press E to Pick up ";
        }
        public void Interact(GameObject interactor)
        {
            // Grab the player's CombatSystem
            playerCombatSystem = interactor.GetComponent<PlayerCombatSystem>();
            if (playerCombatSystem != null)
            {
                if(playerCombatSystem.EquipWeapon(weaponData))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
