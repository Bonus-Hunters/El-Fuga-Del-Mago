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
        [SerializeField] protected float currentHealth = 100f;
        [SerializeField] protected float currentMana = 100f;
        [SerializeField] protected float maxMana = 100f;

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
        protected bool isRunning;

        [Header("Audio Settings")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip walkClip;
        [SerializeField] private AudioClip runClip;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip crouchClip;



        protected virtual void Awake()
        {
            controller = GetComponent<CharacterController>();
            audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        }

        protected virtual void Update()
        {
            ApplyGravity();
            AdjustCrouchHeight();
        }

        public virtual void Move(float horizontal, float vertical)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl)) isCrouching = true;
            if (Input.GetKeyUp(KeyCode.LeftControl)) isCrouching = false;

            float speed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            {
                speed = runningSpeed;
                isRunning = true;
            }
            else isRunning = false;
            if (isCrouching) speed = crouchSpeed;

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * speed * Time.deltaTime);

            PlayMovementSound(move.magnitude, isRunning, isCrouching);
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

                // Play jump sound
                if (audioSource != null && jumpClip != null)
                {
                    audioSource.PlayOneShot(jumpClip);
                }
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
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                //Die();
                Debug.Log("Player has died.");
            }
        }
        private void PlayMovementSound(float moveAmount, bool running, bool crouching)
        {
            if (moveAmount <= 0.1f || audioSource == null)
            {
                audioSource.Stop();
                return;
            }

            AudioClip clipToPlay = walkClip;

            if (running)
                clipToPlay = runClip;
            else if (crouching)
                clipToPlay = crouchClip;


            if (audioSource.clip != clipToPlay || !audioSource.isPlaying)
            {
                audioSource.clip = clipToPlay;

                // Add random start offset between 0 and 30% of the clip length
                audioSource.time = UnityEngine.Random.Range(0f, clipToPlay.length * 0.3f);

                audioSource.Play();
            }
        }
        public void RestoreHealth(float amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }

        public void RestoreMana(float amount)
        {
            currentMana += amount;
            if (currentMana > maxMana)
                currentMana = maxMana;
        }


    }
}
