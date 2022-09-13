using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed;
    public float jumpSpeed;

    public Rigidbody2D player;
    public CapsuleCollider2D myCollider;

    // Start is called before the first frame update
    // Update is called once per frame

    public PhotonView myView;

    public Transform animationManager;

    public TMP_Text nametag;

    bool jump;

    Vector3 startPos;
    private void Start()
    {
        if (!myView.IsMine)
        {
            nametag.text = myView.Owner.NickName;
            Destroy(player);
            Destroy(myCollider);
            Destroy(this);
        }
        else
        {
            nametag.text = PhotonNetwork.NickName;
        }

        startPos = transform.position;
    }

    public float jetpackForce;
    public bool jetpack, jetAddForceBool;
    public float jetpackFuel = 0;


    void Update()
    {
        if (myView.IsMine)
        {
            if (jetpack && Input.GetKey(KeyCode.Space))
            {
                jetAddForceBool = true;
            }
            else if (jetpack)
            {
                jetAddForceBool = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                jump = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gravityReset = true;
            }
        }
    }

    bool gravityReset;

    void FixedUpdate()
    {
        if (!myView.IsMine)
        {
            nametag.text = myView.Owner.NickName;
            Destroy(player);
            Destroy(myCollider);
            Destroy(this);
        }

        if (player.velocity.y < -16f)
        {
            player.gravityScale = 0f;
        }
        else
        {
            if (gravityReset)
            {
                player.gravityScale = 2.5f;
            }
        }
        if  (!gravityReset && player.velocity.y < 1f)
        {
            gravityReset = true;
        }
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05f)
        {
            Vector3 temp = player.velocity; 
            temp = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, temp.y, temp.z);
            player.velocity = temp;

            animationManager.position = new Vector3(Input.GetAxis("Horizontal"), 1, animationManager.position.z);
        }
        else
        {
            Vector3 temp = player.velocity; 
            temp = new Vector3(0, temp.y, temp.z);
            player.velocity = temp;
            animationManager.position = new Vector3(0, 0, animationManager.position.z);
            
        }
        if (jump)
        {
            player.gravityScale = 1f;
            Vector2 temp = player.velocity;
            temp = new Vector2(temp.x, jumpSpeed);
            player.velocity = temp;

            //player.AddForce(jumpSpeed * Vector2.up);
            jump = false;
            isGrounded = false;
            gravityReset = false;
        }

        if (jetAddForceBool)
        {
            player.AddForce(new Vector2 (0, jetpackForce));
        }

        if (MenuManager.me.kill)
        {
            animationManager.position = new Vector3(animationManager.position.x, animationManager.position.y, 1);
            Invoke("CloseKill", 0.2f);
            MenuManager.me.kill = false;
        }
    }


    void CloseKill() 
    {
        animationManager.position = new Vector3(animationManager.position.x, animationManager.position.y, 0);
    }

    //public BoxCollider2D myBox;
    public CapsuleCollider2D myBox;

    public bool isGrounded;

    /*public bool isGrounded()
    {
        float border = 0.01f;
        RaycastHit2D hit = Physics2D.Raycast(myBox.bounds.center, Vector2.down, myBox.bounds.extents.y + border);
        return hit.collider != null;
    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.gameObject.tag == "Trampoline")
        {
            Vector2 temp = player.velocity;

            temp = new Vector2(temp.x, collision.gameObject.GetComponent<Trampoline>().Speed);

            player.velocity = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kill" && !collision.gameObject.GetComponentInParent<PhotonView>().IsMine)
        {
            transform.position = startPos;
        }
    }
}
