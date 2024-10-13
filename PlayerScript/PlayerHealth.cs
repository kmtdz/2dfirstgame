using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 6f;
    public float Health;
    void Start()
    {
        Health = maxHealth;    
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            --Health;
        }    
        if(Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
