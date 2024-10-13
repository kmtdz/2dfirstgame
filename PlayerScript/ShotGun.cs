using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotGun : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject pelletPrefab;
    [SerializeField] GameObject headGun;
    private GameObject pellet;
    public float bullet = 5f;
    public float powerGun = 1f;
    private float dirX;
    private float dirY;
    bool isBullet = true;
    public bool facingRight = true;
    private Controller shotgunForce;

    void Start()
    {
        shotgunForce = FindObjectOfType<Controller>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            facingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            facingRight = false;
        }

        if (Input.GetKey(KeyCode.J) && isBullet)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(bulletProcessup()); // Prioritize shooting upwards
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(bulletProcessdown());
            }
            else if (facingRight)
            {
                StartCoroutine(bulletProcessright());
            }
            else
            {
                StartCoroutine(bulletProcessleft());
            }
        }
    }
    IEnumerator bulletProcessright()
    {
        for (int i = 0; i < bullet; i++)
        {
            float angle = Random.Range(45f, -45f);
            float angleinRadians = angle * (Mathf.PI / 180f);
            float sineValue = Mathf.Sin(angleinRadians);
            float cosineValue = Mathf.Cos(angleinRadians);
            dirX = cosineValue * Mathf.Sqrt(2f);
            dirY = sineValue * Mathf.Sqrt(2f);;
            Quaternion pelletRotation = Quaternion.Euler(0f, 0f, angle);
            pellet = Instantiate(pelletPrefab, headGun.transform.position, pelletRotation); 
            rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(dirX, dirY).normalized * powerGun;
            isBullet = false;
            Destroy(pellet, 0.1f);
        }
        shotgunForce.ShotgunEffect();
        yield return new WaitForSeconds(1.5f);
        isBullet = true;   
    }
    IEnumerator bulletProcessleft()
    {
        for (int i = 0; i < bullet; i++)
        {
            float angle = Random.Range(45f, -45f);
            float angleinRadians = angle * (Mathf.PI / 180f);
            float sineValue = Mathf.Sin(angleinRadians);
            float cosineValue = Mathf.Cos(angleinRadians);
            dirX = cosineValue * Mathf.Sqrt(2f);
            dirY = sineValue * Mathf.Sqrt(2f);;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            pellet = Instantiate(pelletPrefab, headGun.transform.position, rotation);
            rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-dirX, -dirY).normalized * powerGun;
            isBullet = false;
            Destroy(pellet, 0.1f);
        }
        shotgunForce.ShotgunEffect();
        yield return new WaitForSeconds(1.5f);
        isBullet = true;   
    }
    IEnumerator bulletProcessup()
    {
        for (int i = 0; i < bullet; i++)
        {
            float angle = Random.Range(45f, 135f);
            float angleinRadians = angle * (Mathf.PI / 180f);
            float sineValue = Mathf.Sin(angleinRadians);
            float cosineValue = Mathf.Cos(angleinRadians);
            dirX = cosineValue * Mathf.Sqrt(2f);
            dirY = sineValue * Mathf.Sqrt(2f);;
            Quaternion pelletRotation = Quaternion.Euler(0f, 0f, angle);
            pellet = Instantiate(pelletPrefab, headGun.transform.position, pelletRotation); 
            rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(dirX, dirY).normalized * powerGun;
            isBullet = false;
            Destroy(pellet, 0.1f);
        }
        shotgunForce.ShotgunEffect();
        yield return new WaitForSeconds(1.5f);
        isBullet = true;   
    }
    IEnumerator bulletProcessdown()
    {
        for (int i = 0; i < bullet; i++)
        {
            float angle = Random.Range(45f, 135f);
            float angleinRadians = angle * (Mathf.PI / 180f);
            float sineValue = Mathf.Sin(angleinRadians);
            float cosineValue = Mathf.Cos(angleinRadians);
            dirX = cosineValue * Mathf.Sqrt(2f);
            dirY = sineValue * Mathf.Sqrt(2f);;
            Quaternion pelletRotation = Quaternion.Euler(0f, 0f, angle);
            pellet = Instantiate(pelletPrefab, headGun.transform.position, pelletRotation); 
            rb = pellet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-dirX, -dirY).normalized * powerGun;
            isBullet = false;
            Destroy(pellet, 0.1f);
        }
        shotgunForce.ShotgunEffect();
        yield return new WaitForSeconds(1.5f);
        isBullet = true;   
    }
}
