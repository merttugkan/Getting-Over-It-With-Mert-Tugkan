using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTurn : MonoBehaviour
{
    public float rotateAmount;
    void FixedUpdate()
    {
        transform.Rotate(0,0, rotateAmount);
    }
}
