using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Assets.Scripts.Interfaces;
using UnityEditor.UI;
using UnityEngine;
using Assets.Scripts;

public class SkeletonController : MonoBehaviour, IAttackable
{
    // Start is called before the first frame update
    bool isMoving = true, isAttacking = false, isDead = false;
    private float health = 20f;
    WayPointFollower wayPoint;
    public Transform player;
    Animator anim;
    [Header("Player Detection")]
    [SerializeField] float detectionRange = 3f;
    [SerializeField] float dist = 1f;
    [SerializeField] float damageAmount = 2f;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        wayPoint = GetComponent<WayPointFollower>();
        wayPoint.loopPath = true;
        anim.Play("Walk");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Collided with Enemy");

        IAttackable dmg = other.GetComponent<IAttackable>();
        // if projet hit an attackabale object -> [player]
        if (dmg != null)
            dmg.TakeDamage(damageAmount);
    }

    bool checkForAttacks()
    {
        if (player != null)
        {

            dist = Vector3.Distance(transform.position, player.position);
            // start attacking player
            if (dist <= detectionRange)
            {
                wayPoint.StopWalking();
                return true;
            }
        }
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        // Check if player is in attack range
        if (checkForAttacks())
        {
            // attackPlayer();
            isAttacking = true;
            anim.SetBool("IsAttacking", true);   // trigger attack animation
            anim.SetBool("IsWalking", false);
        }
        else
        {
            // Resume walking
            anim.SetBool("IsAttacking", false);

            // Handle walking or idle
            if (!wayPoint.hasFinishedPath)
            {
                wayPoint.ResumeWalking();
                anim.SetBool("IsWalking", true);
                isMoving = true;
            }
            else
            {
                isMoving = false;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsIdle", true);
            }

        }
        // Safety: if not moving and not attacking, ensure idle
        if (!isMoving && !isAttacking)
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", true);

        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Skeleton took " + damage + " damage.");
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Destroy(transform.parent.gameObject);
           // Destroy(gameObject);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsIdle", false);
        }
    }
}
