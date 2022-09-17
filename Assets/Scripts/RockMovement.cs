using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D me;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            Vector2 temp = me.velocity;

            temp = new Vector2(temp.x, collision.gameObject.GetComponent<Trampoline>().Speed);

            me.velocity = temp;
        }
    }
}
