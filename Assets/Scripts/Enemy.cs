using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject GrenadeObject;
    public GameObject BoxObject;
    public Transform LaunchPoint;
    public float VerticalSpeed;
    public float HorizontalSpeed;
    public int Health = 5;

    private Rigidbody2D MyRigidBody;
    private CircleCollider2D MyCollider;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MyRigidBody.velocity = new Vector2(HorizontalSpeed, VerticalSpeed);
    }

    void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //TODO Deal Damage to Player    
            Debug.Log("Enemy hit by bullet");

            //Destroy(gameObject);

            Health -= 1;
        }
    }

    void SpawnBox()
    {
        Instantiate(BoxObject, LaunchPoint);
    }

    void SpawnGrenade()
    {
        Instantiate(GrenadeObject, LaunchPoint);
    }

    void TakeDamage()
    {

    }
}
