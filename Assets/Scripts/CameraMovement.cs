using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform follow;
    public float dampAmount;

    public Camera myCam;

    public Rigidbody2D me;
    void Update()
    {
        float temp = myCam.orthographicSize;
        temp -= Input.GetAxis("Mouse ScrollWheel") * 10;
        if (temp < 1)
        {
            temp = 1;
        }
        else if (temp > 50)
        {
            temp = 25;
        }
        myCam.orthographicSize = temp;

        if (MenuManager.me.myPlayer != null)
        {
            follow = MenuManager.me.myPlayer.transform;
        }
        if (follow != null){
            Vector3 translateAmount = follow.position - transform.position;
            translateAmount.z = 0;
            me.velocity = (translateAmount*dampAmount);
        }

        
    }
}
