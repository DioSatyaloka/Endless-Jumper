using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D playerRB;

    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    public float jumpTime;
    private float jumpTimeCounter;

    public bool grounded;
    public LayerMask whatIsGround;

    private Collider2D myCollider;

    private Animator myAnimator;

	// Use this for initialization
	void Start () {
        playerRB = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

	}
	
	// Update is called once per frame
	void Update () {

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if(grounded)
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            // playerRB.AddRelativeForce(transform.up * jumpForce, ForceMode2D.Force);

        }

        if (Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            if(jumpTimeCounter > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            jumpTimeCounter = 0;
        }

        if(grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        myAnimator.SetFloat("Speed", playerRB.velocity.x);

        myAnimator.SetBool("Grounded", grounded);

    }
}
