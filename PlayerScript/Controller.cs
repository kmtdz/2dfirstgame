using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform raycastStopOrigin;
    public float Speed;
    float jumpForce = 12.5f;
    float maxSpeed = 350f;
    float accurationSpeed= 700f;
    float movement;
    public float forceAmount = 3f;
    public float fallMultiplier = 2.5f;
    
    
    float jumpStartTime =0.25f;
    float jumpTime;
    bool isForcing = true;
    bool isGround = true;
    bool isJumping;
    public bool facingRight = true;
    



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGround)
        {
            isGround = true;
            isForcing = true;   
            Speed = 150f;
            jumpTime = 0f;

        }
        if (collision.gameObject.tag == "Enemy")
        {
            isGround= false;
            isForcing= false;
            rb.AddForce(Vector2.up* forceAmount , ForceMode2D.Impulse);
            
        }
    
    }
    
    void Update()
    {

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (isForcing)
        {
            MovementSetting();
            Jump();
            Gravity();
        }
        


    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGround = false;
        }
        if (Input.GetButtonUp("Jump") && isJumping )
        {
            rb.AddForce(Vector2.down * 12 , ForceMode2D.Impulse);
        }
        if (Input.GetButton("Jump"))
        {
            jumpTime += Time.deltaTime ;
            if (jumpTime > jumpStartTime)
            {
                isJumping = false;
                
            }
            else
            {
                isJumping = true;
            }

        }

    }
    void Gravity()
    {
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //trong luc
        }
    }
    void MovementSetting()
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.D) )
        {
            if (!facingRight)
            {
                transform.Rotate(0, 180, 0);
                facingRight = true;
            }
            else
            {
                facingRight = true;
            }
        }
        else if (Input.GetKey(KeyCode.A) )
        {
            if (facingRight)
            {
                transform.Rotate(0, 180, 0);
                facingRight = false;
            }
            else
            {
                facingRight = false ;
            }
        }



        if (Speed < maxSpeed && movement != 0f)
        {
            Speed += accurationSpeed * Time.deltaTime; // toc do = van toc + gia toc * denta T
        }
        if (movement == 0f)
        {
            Speed = 0f;
        }
    }
    void FixedUpdate()
    {
        
        if (isForcing)
        {
            rb.velocity = new Vector2(movement * Speed * Time.fixedDeltaTime, rb.velocity.y);
        }
        Vector2 raycastDirection = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hitStop = Physics2D.Raycast(raycastStopOrigin.position, raycastDirection, 0.3f);
        Debug.DrawRay(raycastStopOrigin.position, raycastDirection * 0.3f, Color.red);
        if(hitStop && hitStop.collider.tag == "Enemy")
        {
            Speed =0 ;
        }
    }

    public void ShotgunEffect()
    {
        if (facingRight)
        {
            isForcing = false;
            isGround = false;
            rb.AddForce(new Vector2(-1,1).normalized * 6, ForceMode2D.Impulse);
            Invoke("ResetForcing",0.5f);
        }
        else if(!facingRight)
        {
            isForcing = false;
            isGround = false;
            rb.AddForce(new Vector2(1,1).normalized* 6, ForceMode2D.Impulse);
            Invoke("ResetForcing",0.5f);
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (Input.GetKey(KeyCode.W))
            {
                isForcing = false;
                isGround = false;
                rb.AddForce(new Vector2(0,-1).normalized* 6, ForceMode2D.Impulse);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                isForcing = false;
                isGround = false;
                rb.AddForce(new Vector2(0,1).normalized* 6, ForceMode2D.Impulse);
            }
        }
    }

    void ResetForcing()
    {
        isForcing = true;
        Speed = 0f;
    }
}


