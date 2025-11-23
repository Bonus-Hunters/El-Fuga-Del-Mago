using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.collider.tag == "Player")
        {
            print("boss collided with player");
        }
    }
}

