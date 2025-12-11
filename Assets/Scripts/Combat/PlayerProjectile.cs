using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private bool collided;
    public float damage = 20f;
    public GameObject impactVFX;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point,Quaternion.identity) as GameObject;
            if(collision.gameObject.layer == LayerMask.NameToLayer("Hittable") )
            {
                Debug.Log("hit  a skeleton");
                IAttackable takeHit = collision.gameObject.GetComponent<IAttackable>();
                takeHit.TakeDamage(damage);
            }

            Destroy(impact,2);
            Destroy(gameObject);
        }
    }
}
