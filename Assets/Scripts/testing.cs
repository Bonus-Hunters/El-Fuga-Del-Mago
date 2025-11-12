using Assets.Scripts.Interfaces;
using Assets.Scripts.Player.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class testing : MonoBehaviour, IAttackable
    {
        public float health = 50f;

        public void TakeDamage(float damage)
        {
            health -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage! Remaining HP: {health}");

            if (health <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            Debug.Log($"{gameObject.name} has been destroyed!");
            Destroy(gameObject);
        }
    }
}
