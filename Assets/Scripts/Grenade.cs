using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody2D MyRigidBody2D;
    public CircleCollider2D MyCollider2D;
    public float MoveSpeed;
    public bool Moving;
    public Vector2 ExplosionForce;
    public int AttackDamage;
    PlayerHealth playerHealth;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        MyCollider2D = GetComponent<CircleCollider2D>();
    }


    private void Awake()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            MyRigidBody2D.velocity = new Vector2(-MoveSpeed, MyRigidBody2D.velocity.y);
        }

        else
        {

        }       
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 ImpactPoint = collision.GetContact(0).point;

            collision.rigidbody.AddForceAtPosition(ExplosionForce, ImpactPoint);

            PlayerHealth PlayerHealthComp = collision.gameObject.GetComponent<PlayerHealth>();

            //TODO Deal Damage to Player    
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(AttackDamage);
                Destroy(gameObject);
            }

        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
