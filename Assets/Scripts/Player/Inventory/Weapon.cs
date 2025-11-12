using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player.Inventory
{
    public abstract class Weapon
    {
        public EquippableItem DataItem;

        //For Hitbox Sizing
        public float width;
        public float height;

        public float range;
        public float damage;
        public float attackDuration;
        public float attackSpeed;
        public float attackTime;
        public float cooldownTime;
        public Weapon(EquippableItem data)
        {
            DataItem = data;
            damage = 10f;                   // instance-specific override
            range = 2f;                     // instance-specific override
            attackSpeed = 1f;               // instance-specific override
            width = 2f;
            height = 2f;
            attackDuration = 5f;
            attackTime = 0f;                // runtime tracking
            cooldownTime = 0f;              // runtime tracking
        }

        public abstract void Attack();
    }
}
