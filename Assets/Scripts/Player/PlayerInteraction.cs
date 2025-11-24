using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactDistance = 3f;
        [SerializeField] private LayerMask interactLayer;
        [SerializeField] private TMP_Text interactionText;
        [SerializeField] private Camera playerCamera;

        private IInteractable currentInteractable;

        public bool IsInteracting { get; private set; }
        private void Start()
        {
            interactionText.enabled = false;
        }

        private void Update()
        {
            RayInteractCheck();
            HandleInteractionInput();
        }

        private void RayInteractCheck()
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            // Draw the ray in Scene view
            if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.green);

                currentInteractable = hit.collider.GetComponent<IInteractable>();
                if (currentInteractable != null)
                {
                    //Debug.Log("Looking at interactable: " + hit.collider.name);
                    interactionText.text = currentInteractable.InterationPrompt();
                    interactionText.enabled = true;
                }
                else
                {
                    interactionText.enabled = false;
                }
            }
            else
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.red);
                currentInteractable = null;
                interactionText.enabled = false;
            }
        }
        private void HandleInteractionInput()
        {
            if (currentInteractable == null) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentInteractable.Interact(gameObject);
                IsInteracting = true;
            }
        }
        private void EndInteraction()
        {
            IsInteracting = false;
        }
    }
}
