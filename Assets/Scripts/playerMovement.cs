using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.XR;

public class playerMovement : MonoBehaviour, IAttackable
{
    public float speed = 3f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void IAttackable.TakeDamage(float damage)
    {
        Debug.Log("Player Got hit!");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical);
        controller.Move(move.normalized * speed * Time.deltaTime);
    }

}
