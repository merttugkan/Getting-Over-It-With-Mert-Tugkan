using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSync : MonoBehaviour
{
    public Animator controller;
    public SpriteRenderer myrenderer;

    public Transform anim;
    public GameObject kill;


    void Update()
    {
        if (anim.transform.position.y > 0.1f)
        {
            controller.SetBool("Walking", true);

            
            if (anim.transform.position.x < 0)
            {
                myrenderer.flipX = true;
            }
            else
            {
                myrenderer.flipX = false;
            }
        }
        else
        {
            controller.SetBool("Walking", false);
        }

        if (anim.transform.position.z > 0.1f)
        {
            kill.SetActive(true);
        }
        else
        {
            kill.SetActive(false);
        }
    }
}
