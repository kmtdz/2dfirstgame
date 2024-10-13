using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform raycastFlip;
    public Transform raycastCheck; 
    public float dirX = 1f;
    public float Speed = 1f;
    public float forceAmount = 1f;
    float raycastDistance = 1f;
    bool facingRight = true;
    bool keepRunning = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if(keepRunning)
        {
            rb.velocity = new Vector2(Speed * dirX, rb.velocity.y);
        }
    }
    void FixedUpdate()
    {
        Vector2 raycastCDirection = facingRight ? Vector2.left : Vector2.right;
        RaycastHit2D hitF = Physics2D.Raycast(raycastFlip.position, Vector2.down, raycastDistance);
        Debug.DrawRay(raycastFlip.position, Vector2.down * raycastDistance, Color.red);
        RaycastHit2D hitC = Physics2D.Raycast(raycastCheck.position, raycastCDirection, 0.3f);
        Debug.DrawRay(raycastCheck.position, raycastCDirection * 0.3f, Color.red);

        if (hitF.collider == null)
        {
            Flip();
        }
        if(hitC && hitC.collider.tag == "Player" )
        {
            Flip();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            keepRunning = false;
            rb.AddForce(new Vector2(-dirX, 1).normalized * forceAmount, ForceMode2D.Impulse);
        }
        if(collision.gameObject.tag == "Ground")
        {
            keepRunning = true ;
        }
    }
    void Flip()
    {
        dirX = - dirX;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

}
