using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    //[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
    public abstract class Weapon : ScriptableObject
    {
        public EquippableItem DataItem;

        public float range;
        public float damage;
        public float attackSpeed;
        public float cooldownTime;

        public LayerMask hitLayers;
        public Transform attackOrigin;

        public MonoBehaviour CoroutineHost;


        public Animator WeaponAnimator;

        public Weapon(EquippableItem data) { DataItem = data; }

        public void SetAnimator(Animator animator)
        {
            WeaponAnimator = animator;
            //WeaponAnimator.SetBool("equip", true);
        }

        public abstract void Attack();
    }
}
