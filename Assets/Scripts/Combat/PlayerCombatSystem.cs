using Assets.Scripts.Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class PlayerCombatSystem : MonoBehaviour
    {
        [SerializeField] Transform WeaponSlot;
        public Weapon equippedWeapon;
        public Transform attackOrigin;
        private GameObject weaponObjectInstance;

        void Start()
        {
            equippedWeapon = null;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && equippedWeapon != null)
            {
                //Debug.Log("Player attacking with " + equippedWeapon.name);
                equippedWeapon.Attack();
                //Debug.Log("Attack finished.");
            }
        }
        public bool EquipWeapon(Weapon newWeapon)
        {
            if (equippedWeapon != null)
            {
                Debug.Log("Already Have a Weapon");
                return false;
            }

            equippedWeapon = newWeapon;

            // Assign the coroutine host HERE (correct timing)
            equippedWeapon.CoroutineHost = this;
            
            equippedWeapon.attackOrigin = attackOrigin;

            if (newWeapon.DataItem.prefab != null)
            {
                Debug.Log("Spawning weapon prefab for: " + newWeapon.DataItem.name);
                weaponObjectInstance = Instantiate(newWeapon.DataItem.prefab, WeaponSlot);
                weaponObjectInstance.transform.localPosition = newWeapon.DataItem.equipPositionOffset;
                weaponObjectInstance.transform.localEulerAngles = newWeapon.DataItem.equipRotationOffset;


                // Disable Rigidbody if it exists
                Rigidbody rb = weaponObjectInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            }

            Debug.Log("Equipped weapon: " + newWeapon.DataItem.name);
            return true;
        }
        public void DropWeapon()
        {
            if (equippedWeapon == null)
                return;

            Vector3 dropPos = transform.position + transform.forward * 1f + Vector3.up * 0.5f;
            // Spawn pickup prefab
            GameObject dropped = Instantiate(equippedWeapon.DataItem.prefab, dropPos, Quaternion.identity);

            Rigidbody rb = dropped.GetComponent<Rigidbody>();
            
            if(rb == null)
                dropped.AddComponent<Rigidbody>();
            
            dropped.AddComponent<BoxCollider>();
            WeaponHandler handler = dropped.AddComponent<WeaponHandler>();
            handler.weaponData = equippedWeapon;
            dropped.layer = LayerMask.NameToLayer("Interactable");

            // Remove in-hand visual
            if (weaponObjectInstance != null)
                Destroy(weaponObjectInstance);

            Debug.Log("Dropped weapon: " + equippedWeapon.DataItem.name);

            // Clear equipped weapon
            equippedWeapon = null;
        }
    }
}
