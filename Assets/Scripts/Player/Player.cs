using Assets.Scripts.Abstract;
using Assets.Scripts.Combat;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : Character, IAttackable
    {
        [Header("Camera")]
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float cameraPitch = 0f;
        [SerializeField] private float mouseSensitivity = 2f;
        public static bool playerInUI = false;
        private PlayerCombatSystem playerCombatSystem;

        [Header("Stats")]
        public Image healthFill;
        public Image manaFill;
        protected void Start()
        {
            playerCombatSystem = GetComponent<PlayerCombatSystem>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void Update()
        {
            if (currentHealth <= 0) return;
            base.Update();
            UpdateStatsUI();
            //Debug.Log("player health " + currentHealth);
            // If UI is open, do not process gameplay input
            if (!playerInUI)
                HandleInput();

            // Cursor control depending on UI state
            if (playerInUI)
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
            if (playerInUI) return;
            base.Rotate(mouseX, mouseY);

            // vertical look (pitch)
            cameraPitch -= mouseY * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -80f, 80f);
            playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        }

        void IAttackable.TakeDamage(float damage)
        {
            // adjust health reduction and death conditions 
            currentHealth -= damage;
            if (currentHealth <= 0)
            {

                currentHealth = 0;
                Debug.Log("Player is Dead!!!!");
                ReloadGameScene.Instance.PlayerDied();
            }

        }
        private void UpdateStatsUI()
        {
            float fill = currentHealth / maxHealth;
            healthFill.fillAmount = fill;
            fill = currentMana / maxMana;
            manaFill.fillAmount = fill;
        }
        public float GetMana() { return currentMana; }
        public void SetMana(float mana) { currentMana = mana; }
    }
}
