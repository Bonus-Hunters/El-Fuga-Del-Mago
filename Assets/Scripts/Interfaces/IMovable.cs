using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(float horizontal, float vertical);
    void Rotate(float x, float y);
}
