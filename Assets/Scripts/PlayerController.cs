using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float acceleration;
    public bool grounded;
    public int jumpheight;
    public LayerMask WhatIsGround;


    [Header("Shooting")]
    public string fireKey = "Fire1";
    public GameObject bulletPrefab;
    [SerializeField, Tooltip("Shots per minute.")] private float rateOfFire = 1f;
    public Transform socket;
    private float timeBetweenShots;
    private float lastTimeFired;

    private Rigidbody2D rb2d;
    private Collider2D myCollider;
    private Animator MyAnimator;
    //private bool moving = false;
    //private float t = 0.0f;
    //private Vector2 movement;
    //private bool hit;


    #region Properties

    public float RateOfFire
    {
        get { return rateOfFire; }
        set
        {
            rateOfFire = value;
            timeBetweenShots = 60f / rateOfFire;
        }
    }

    #endregion Properties

    void Awake()
    {

        rb2d = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<Collider2D>();

        MyAnimator = GetComponent<Animator>();

        RateOfFire = rateOfFire;

    }

    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround);

        Jump();

        if (Input.GetButton(fireKey))
        {

            Shoot();
        }


        MyAnimator.SetFloat("Speed", rb2d.velocity.x);
        MyAnimator.SetBool("Grounded", grounded);
    }

    private void Shoot()
    {
        if (lastTimeFired + timeBetweenShots <= Time.time)
        {

            Bullet bullet = Instantiate(bulletPrefab, socket.position, Quaternion.identity)?.GetComponent<Bullet>();
            if (bullet != null)
            {

                bullet.Shoot();
            }
            else
            {
                Debug.Log("Bullet script missing.");
            }

            lastTimeFired = Time.time;
        }
    }

    void FixedUpdate()
    {

        //Vector2 movement = new Vector2(1f, 0f);
        //rb2d.AddForce(movement * speed);
        //Debug.Log(movement * speed);

        //rb2d.transform.Translate(Vector3.right * Time.deltaTime * speed);

        speed = speed + acceleration;


        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }

    private void Slow()
    {


        speed = speed - 1f;

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded == true)
            {

                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpheight);
                print("jump");
                
                /*rb2d.velocity = new Vector2(0.0f, 1.0f * jumpheight);
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
                }*/

            }

        }
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            grounded = true;

        }


        if (col.collider.CompareTag("Obstacle"))
        {
            hit = true;
            Slow();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            grounded = false;
        }
        if (col.collider.CompareTag("Obstacle"))
        {
            hit = false;
        }
    }*/
}
