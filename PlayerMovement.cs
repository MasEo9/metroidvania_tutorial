using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    //required comps for animations and physics 
    private Rigidbody2D rb2d;
    private Animator myAnimator;
    
    //variables to play with
    public float speed = 2.0f;
    public float horizMovement; //= 1[or]-1[or]0
    private void Start()
    {
        rb2d = GetComponent< Rigidbody2D > ();
        myAnimator = GetComponent<Animator>();
    }
    
    //handles the input for physics 
    private void Update()
    {
        //check direction given by player
        horizMovement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //move the charater left and right,
        rb2d.velocity = new Vector2(horizMovement * speed, rb2d.velocity.y);
    }
}
