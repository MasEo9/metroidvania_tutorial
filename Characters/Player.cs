using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float runSpeed = 2.0f;
    private float walkSpeed = 1.0f;
    private float teleDistTimeStart;
    private float teleTimeMod = 10.0f;
    private float minTeleDist = 0.0f;
    private float maxTeleDist = 3.0f;


    public override void Start()
    {
        base.Start();
        speed = runSpeed;
    }
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        //Debug.Log(direction);
        HandleJumping();
        HandleTeleport();
    }
    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed", Mathf.Abs(direction));
        TurnAround(direction);
    }

    protected override void HandleJumping()
    {
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.SetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        if(Input.GetButton("Jump") && !stoppedJumping && jumpTimeCounter > 0)
        {

            Jump();
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
    }
    protected override void HandleTeleport()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            teleDistTimeStart = Time.time;
        }
        if(Input.GetButtonUp("Fire1"))
        {
            teleDistance = (Time.time - teleDistTimeStart) * teleTimeMod;
            teleDistance = Mathf.Clamp(teleDistance, minTeleDist, maxTeleDist);
            Debug.Log(teleDistance);
            Teleport();
        }
    }
}
