using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D rb;
    float raycastDistance = 0.01f;
    public Transform raycastOrigin1;
    public Transform raycastOrigin2;
    public Transform raycastOrigin3;
    public float flySpeed = 1f;
    bool facingRight = true;
    public float dirX = 1f;
    public float dirY = 0.25f;

    




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = new Vector2(dirX,dirY) * flySpeed;
    }

    void FixedUpdate()
    {
        Vector2 raycastDirection1 = facingRight ? Vector2.right : Vector2.left;    
        
        RaycastHit2D hit1 =  Physics2D.Raycast(raycastOrigin1.position , raycastDirection1 , raycastDistance);
        RaycastHit2D hit2 = Physics2D.Raycast(raycastOrigin2.position, Vector2.up, raycastDistance);
        RaycastHit2D hit3 = Physics2D.Raycast(raycastOrigin3.position, Vector2.down, raycastDistance);


        Debug.DrawRay(raycastOrigin1.position,raycastDirection1*raycastDistance,Color.red);
        Debug.DrawRay(raycastOrigin2.position, Vector2.up * raycastDistance, Color.red);
        Debug.DrawRay(raycastOrigin3.position, Vector2.down * raycastDistance, Color.red);
        if (hit1 && hit1.collider.tag == "Ground" && facingRight)
        {
            Flip();
        }
        else if (hit1 && hit1.collider.tag == "Ground" && !facingRight)
        {
            Flip();
        }
        if(hit2 && hit2.collider.tag == "Ground")
        {
            dirY = -0.25f;
            
        }
        else if(hit3 && hit3.collider.tag == "Ground")
        {
            dirY = 0.25f;
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector2(0,180));
        dirX = -dirX;   
    }
}
