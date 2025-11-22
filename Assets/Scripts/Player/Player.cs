using Assets.Scripts.Abstract;
using Assets.Scripts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : Character
    {
        [Header("Camera")]
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float cameraPitch = 0f;
        [SerializeField] private float mouseSensitivity = 2f;

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
            HandleInput();
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
            if(Input.GetKeyDown(KeyCode.Q))
                playerCombatSystem.DropWeapon();
        }
        public override void Rotate(float mouseX, float mouseY)
        {
            base.Rotate(mouseX, mouseY);

            // vertical look (pitch)
            cameraPitch -= mouseY * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
            playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }
}
