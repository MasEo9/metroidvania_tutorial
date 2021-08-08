using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public abstract class Character : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float direction;

    protected bool facingRight = true;
    protected bool noMovement = true;

    [Header("Jump Details")]
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float jumpTime;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float radOCircle;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected bool grounded;
    protected float teleDistance;
    protected float jumpTimeCounter;
    protected bool stoppedJumping;
    protected bool stoppedTele;

    //[Header("Attack Variables")]

    //[Header("Character Stats")]
    protected Rigidbody2D rb;
    protected Animator myAnimator;
    #region monos
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }
    public virtual void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);
        if(rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true); 
        }
    }
    public virtual void FixedUpdate()
    {
        // handle mech/physics 
        HandleMovement();
        HandleLayers();
    }
    #endregion

    #region mechanics
    protected void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    protected void Teleport()
    {
        rb.transform.position = new Vector2(rb.transform.position.x + teleDistance * rb.transform.localScale.x, rb.transform.position.y);
    }
    #endregion

    #region subMechanics
    protected abstract void HandleJumping();
    protected abstract void HandleTeleport();
    protected virtual void HandleMovement()
    {
        Move();
    }
    protected void TurnAround(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
    protected void HandleLayers()
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
    #endregion

    #region visdebugs
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }
    #endregion
}