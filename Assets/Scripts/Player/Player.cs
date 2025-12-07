using Assets.Scripts.Abstract;
using Assets.Scripts.Combat;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : Character, IAttackable
    {
        [Header("Camera")]
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float cameraPitch = 0f;
        [SerializeField] private float mouseSensitivity = 2f;
        public bool IsInUI = false;
        private PlayerCombatSystem playerCombatSystem;

        protected void Start()
        {
            playerCombatSystem = GetComponent<PlayerCombatSystem>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void Update()
        {
            base.Update();
            // If UI is open, do not process gameplay input
            if (!IsInUI)
                HandleInput();

            // Cursor control depending on UI state
            if (IsInUI)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        private void HandleInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Move(horizontal, vertical);
            Rotate(mouseX, mouseY);

            if (Input.GetButtonDown("Jump"))
                Jump();
            if (Input.GetKeyDown(KeyCode.Q))
                playerCombatSystem.DropWeapon();
        }
        public override void Rotate(float mouseX, float mouseY)
        {
            if (IsInUI) return;
            base.Rotate(mouseX, mouseY);

            // vertical look (pitch)
            cameraPitch -= mouseY * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
            playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }

        void IAttackable.TakeDamage(float damage)
        {
            // adjust health reduction and death conditions 
            Debug.Log("Player Got Hit!");
        }
    }
}
