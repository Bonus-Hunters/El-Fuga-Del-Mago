using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character : MonoBehaviour, IMovable
    {
        [Header("Movement Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float rotationSpeed = 2f;
        [SerializeField] protected float gravity = -9.81f;
        [SerializeField] protected float jumpForce = 5f;
        [SerializeField] protected float RaycastDown = 1.1f;

        protected CharacterController controller;
        protected Vector3 velocity;
        protected bool isGrounded;

        protected virtual void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            ApplyGravity();
        }

        public virtual void Move(float horizontal, float vertical)
        {
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * moveSpeed * Time.deltaTime);
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
    }
}
