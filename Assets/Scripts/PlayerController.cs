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


    [Header("Shooting")]
    public string fireKey = "Fire1";
    public GameObject bulletPrefab;
    [SerializeField, Tooltip("Shots per minute.")] private float rateOfFire = 1f;
    private float timeBetweenShots;
    private float lastTimeFired;

    private Rigidbody2D rb2d;
    private bool moving = false;
    private float t = 0.0f;
    private Vector2 movement;
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
        RateOfFire = rateOfFire;

    }

    void Update()
    {
        Jump();

        if (Input.GetButton(fireKey))
        {

            Shoot();
        }
    }

    private void Shoot()
    {
        if (lastTimeFired + timeBetweenShots <= Time.time)
        {

            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity)?.GetComponent<Bullet>();
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

        rb2d.transform.Translate(Vector3.right * Time.deltaTime * speed);

        speed = speed + acceleration;


        //rb2d.velocity = new Vector2(speed, t);
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
    }
}
