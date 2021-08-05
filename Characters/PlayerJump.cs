using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerJump : MonoBehaviour
{
    //force, apply force, 1x 

    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;


    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radOCircle;
    [SerializeField] private LayerMask whatIsGround;
    public bool grounded;


    [Header("Components")]
    private Rigidbody2D rb; 
    private Animator myAnimator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;

    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.SetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        if(Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        if(Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
            myAnimator.SetTrigger("jump");
        }

        if(rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if(!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        { 
            myAnimator.SetLayerWeight(1, 0); 
        }
    }
}
