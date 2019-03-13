﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    [Tooltip ("1 = On ground and can jump, 0 = In air")]public int grounded;
    public int jumpheight;

    private Rigidbody2D rb2d;
    private bool moving = false;
    private float t = 0.0f;
    private Vector2 movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        
    }

    
    void FixedUpdate()
    {

        //Vector2 movement = new Vector2(1f, 0f);
        //rb2d.AddForce(movement * speed);
        //Debug.Log(movement * speed);

        rb2d. transform.Translate(Vector3.right * Time.deltaTime * speed);
        

        //rb2d.velocity = new Vector2(speed, t);
    }


    private void Jump()
    {
        if (grounded >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2d.velocity = new Vector2(0.0f, 1.0f * jumpheight);
                moving = true;
                t = 0.0f;
                print("Jump");
               

                if (moving)
                {
                   
                    t = t + Time.deltaTime;
                    if (t > 1.0f)
                    {
                        Debug.Log(gameObject.transform.position.y + " : " + t);
                        t = 0.0f;
                    }
                }

            }

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            grounded++;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            grounded--;
    }
}
