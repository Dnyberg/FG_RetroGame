using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float acceleration;
    public float maxSpeed;
    public float minSpeed;
    public bool grounded;
    public int jumpheight;
    public LayerMask WhatIsGround;

    [SerializeField, Tooltip("Number of units it takes to speed up the player with ACCELERATION. ACCELERATION also increases this value.")] public float speedIncreaseMilestone;
    public Vector2 hitForce;
    public float ExplosionPower;
    public float ExplosionRadius;


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
    private float speedMilestoneCount;
    //private bool moving = false;
    //private float t = 0.0f;
    //private Vector2 movement;
    private bool hit;


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

        speedMilestoneCount = speedIncreaseMilestone;

    }

    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, WhatIsGround);

        Jump();

        if (Input.GetButton(fireKey))
        {

            Shoot();
        }

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * acceleration;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed) * acceleration;
            MyAnimator.SetFloat("SpeedMultiplier", speed / 20f);
        }

        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);


        MyAnimator.SetFloat("Speed", rb2d.velocity.x);
        MyAnimator.SetBool("Grounded", grounded);
        MyAnimator.SetBool("Hit", hit);
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

    /*void FixedUpdate()
    {

        //Vector2 movement = new Vector2(1f, 0f);
        //rb2d.AddForce(movement * speed);
        //Debug.Log(movement * speed);

        //rb2d.transform.Translate(Vector3.right * Time.deltaTime * speed);

        //speed = speed + acceleration;

      
    }*/

    /*private void Hit()
    {

        Vector2 ImpactPoint = collision.GetContact(0).point;

        rb2d.AddForceAtPosition(hitForce, ImpactPoint);
        

    }*/

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

    void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.collider.CompareTag("Ground"))
        {
            grounded = true;

        }*/
        //PlayerController PlayerControllerComp = col.gameObject.GetComponent<PlayerController>();

        //Vector2 ImpactPoint = col.GetContact(0).point;

        


        if (col.collider.CompareTag("Obstacle"))
        {
            hit = true;
            //speed = 0f;
            Debug.Log("Hit");
            //rb2d.velocity = new Vector2(-100f, rb2d.velocity.y);

            //AddExplosionForce(col.rigidbody, ExplosionPower, ImpactPoint, ExplosionRadius);
            //rb2d.AddForceAtPosition(hitForce, ImpactPoint);
            //Hit();
            //  rb2d.transform.Translate(Vector3.right * Time.deltaTime * -100f);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
       /* if (col.collider.CompareTag("Ground"))
        {
            grounded = false;
        }*/
        if (col.collider.CompareTag("Obstacle"))
        {
            hit = false;
            //speed = 15f;
        }
    }

    public static void AddExplosionForce(Rigidbody2D rb2d, float Force, Vector3 Position, float Radius)
    {
        var Direction = (rb2d.transform.position - Position);
        float Calc = 1 - (Direction.magnitude / Radius);
        if (Calc <= 0)
        {
            Calc = 0;
        }
        rb2d.AddForce(Direction.normalized * Force * Calc);
    }
}
