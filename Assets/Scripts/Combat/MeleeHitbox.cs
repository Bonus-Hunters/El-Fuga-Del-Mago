using Assets.Scripts.Player.Inventory;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    internal class MeleeHitbox : MonoBehaviour
    {
        private BoxCollider boxCollider;
        private Weapon currentWeapon;
        private GameObject hitbox; // store reference

        private void Awake()
        {
            // Create the hitbox dynamically
            hitbox = new GameObject("AttackHitbox");
            hitbox.transform.parent = transform; // attach to player
            hitbox.transform.localPosition = Vector3.zero;

            boxCollider = hitbox.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true; // Set as trigger to detect hits

            hitbox.SetActive(false); // disabled by default
        }

        public void Setup(Weapon weapon)
        {
            if (weapon == null)
            {
                Debug.Log("Hello");
                return;
            }

            currentWeapon = weapon;
            boxCollider.size = new Vector3(weapon.width, weapon.height, weapon.range);
            hitbox.transform.localPosition = new Vector3(0, (weapon.height / 2) - 0.4f, weapon.range / 2);
        }

        public void Activate()
        {
            if (currentWeapon == null) return;
            hitbox.SetActive(true);
            Invoke(nameof(Deactivate), currentWeapon.attackDuration);
        }

        private void Deactivate()
        {
            hitbox.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log($"Hit {other.name} for {currentWeapon.damage} damage!");
            }
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null)
                return;

            Gizmos.color = Color.red;
            Vector3 center = boxCollider.transform.position + boxCollider.center;
            Vector3 size = boxCollider.size;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
