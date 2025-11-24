using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character : MonoBehaviour, IMovable, IAttackable
    {
        [Header("Health Settings")]
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float Mana = 100f;

        [Header("Movement Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float runningSpeed = 6.9f;
        [SerializeField] protected float rotationSpeed = 2f;
        [SerializeField] protected float gravity = -251f;
        [SerializeField] protected float jumpForce = 2f;
        [SerializeField] protected float RaycastDown = 0.99f;

        [Header("Crouch Settings")]
        [SerializeField] protected float crouchTransSpeed = 6f;
        [SerializeField] protected float crouchSpeed = 2f;
        [SerializeField] protected float crouchHeight = 1.2f;
        [SerializeField] protected float standHeight = 2f;

        protected CharacterController controller;
        protected Vector3 velocity;
        protected bool isGrounded;
        protected bool isCrouching;
        protected bool isJumping;
        protected bool isRunning;



        protected virtual void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            ApplyGravity();
            AdjustCrouchHeight();
        }

        public virtual void Move(float horizontal, float vertical)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl)) isCrouching = true;
            if(Input.GetKeyUp(KeyCode.LeftControl)) isCrouching = false;

            float speed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && !isCrouching) speed = runningSpeed;
            if (isCrouching) speed = crouchSpeed;

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * speed * Time.deltaTime);
        }

        public virtual void Rotate(float mouseX, float mouseY)
        {
            transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        }

        protected virtual void ApplyGravity()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, RaycastDown + 0.1f);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        public virtual void Jump()
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
        public void AdjustCrouchHeight()
        {
            // --- Smoothly adjust capsule height ---
            float targetHeight = isCrouching ? crouchHeight : standHeight;
            float previousHeight = controller.height;

            controller.height = Mathf.MoveTowards(controller.height, targetHeight, crouchTransSpeed * Time.deltaTime);
            float heightDiff = controller.height - previousHeight;
            controller.center -= new Vector3(0, heightDiff / 2, 0);
        }

        public void TakeDamage(float damage)
        {
            maxHealth -= damage;
            if (maxHealth <= 0)
            {
                //Die();
                Debug.Log("Player has died.");
            }
        }
    }
}
