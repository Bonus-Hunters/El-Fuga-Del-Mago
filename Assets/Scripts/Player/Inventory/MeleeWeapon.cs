using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    [CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Inventory/MeleeWeapon")]
    public class MeleeWeapon : Weapon
    {
        [NonSerialized]
        bool attacking = false;
        public MeleeWeapon(EquippableItem data) : base(data) { }
        public override void Attack()
        {
            if (attacking)
            {
                Debug.Log("Weapon is on cooldown, cannot attack yet.");
                return;
            }
            attacking = true;

            //Debug.Log("Attacking " + DataItem.name + " for " + damage + " damage!");

            PerformAttackRaycast();

            // Start cooldown timer instead of resetting immediately
            CoroutineHost.StartCoroutine(ResetAttackRoutine());

            //Debug.Log("Next attack available at: " + nextAttack + " " + attacking);
        }
        private void PerformAttackRaycast()
        {
            if (attackOrigin == null)
            {
                Debug.LogWarning("Attack origin not assigned for weapon!");
                return;
            }

            Debug.DrawRay(attackOrigin.position, attackOrigin.forward * range, Color.red, 0.5f);

            if (Physics.Raycast(attackOrigin.position, attackOrigin.forward, out RaycastHit hit, range, hitLayers))
            {
                Debug.Log($"Hit {hit.collider.name} for {damage} damage!");

                // If the object can take damage
                IAttackable target = hit.collider.GetComponent<IAttackable>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            else
            {
                Debug.Log("Missed attack.");
            }
        }
        protected virtual void ResetAttack()
        {
            attacking = false;
            Debug.Log("Weapon ready again.");
        }
        private IEnumerator ResetAttackRoutine()
        {
            yield return new WaitForSeconds(cooldownTime);

            attacking = false;
            Debug.Log("Weapon ready again.");
        }
    }
}
