using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    bool hasRun;
    bool hasClimb;
    float myGravityScale;
    bool isAlive = true;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>(); 
        myFeetCollider = GetComponent<BoxCollider2D>();
        myGravityScale = myrigidbody.gravityScale;       
    }

    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue inputValue)
    {
        if(!isAlive) { return; }
        moveInput = inputValue.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnFire(InputValue inputValue)
    {
        if(!isAlive) { return; };
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnJump(InputValue inputValue)
    {
        if(!isAlive) { return; }
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if(inputValue.isPressed)
            {
                myrigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
        else return;
    }

    void Run()
    {
        if(!isAlive) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        myAnimator.SetBool("isRunning", hasRun);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
            hasRun = true;
        }  
        else
        {
            hasRun = false;
        }    
    }
    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        { 
            hasClimb = false;
            myAnimator.SetBool("isClimbing", hasClimb);
            myrigidbody.gravityScale = myGravityScale;
            return;
        }        
        myrigidbody.gravityScale = 0;
        Vector2 climbVelocity = new Vector2(myrigidbody.velocity.x, moveInput.y * climbSpeed);
        myrigidbody.velocity = climbVelocity;
        if(Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon)
        {
            hasClimb = true;
            
        } 
        else { hasClimb = false;}
        myAnimator.SetBool("isClimbing", hasClimb);
        // bool playerVerticalSpeed = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon
        // myAnimator.SetBool("isClimbing", playerVerticalSpeed);
    }
    //void Attack
    void Die()
    {
        if(myrigidbody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myrigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
