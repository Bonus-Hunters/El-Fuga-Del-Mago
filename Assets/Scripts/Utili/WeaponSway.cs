using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.Utili
{
    public class WeaponSway : MonoBehaviour
    {
        [Header("Sway Settings")]
        [SerializeField] private float smooth;
        [SerializeField] private float swayMultiplier;


        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * swayMultiplier;
            float mouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
    }
}
