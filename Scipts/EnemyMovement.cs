using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    BoxCollider2D myHandCollider;
    Rigidbody2D myRigidbody2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myHandCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag != "Player")
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }    
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(MathF.Sign(myRigidbody2D.velocity.x)), 1f);
    }
}
