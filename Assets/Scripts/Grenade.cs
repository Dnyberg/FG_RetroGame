using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody2D MyRigidBody2D;
    public CircleCollider2D MyCollider2D;
    public float MoveSpeed;
    public bool MovingLeft;
    public float ExplosionPower;
    public float ExplosionRadius;
    public int AttackDamage;
    public Vector2 ExplosionVector;
    PlayerHealth playerHealth;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        MyRigidBody2D = GetComponent<Rigidbody2D>();
        MyCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingLeft)
        {
            MyRigidBody2D.velocity = new Vector2(-MoveSpeed, MyRigidBody2D.velocity.y);
        }

        else
        {
            MyRigidBody2D.velocity = new Vector2(MoveSpeed, MyRigidBody2D.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }

            if (collision.gameObject.CompareTag("Player"))
           {

            //PlayerController PlayerControllerComp = collision.gameObject.GetComponent<PlayerController>();

            //if (PlayerControllerComp != null)
            //{

            //}

            PlayerHealth PlayerHealthComp = collision.gameObject.GetComponent<PlayerHealth>();

            if (PlayerHealthComp != null)
            {
                if (PlayerHealthComp.currentHealth > 0)
                {
                    PlayerHealthComp.TakeDamage(AttackDamage);
                }
            }

            Vector2 ImpactPoint = collision.GetContact(0).point;

          //  AddExplosionForce(collision.rigidbody, ExplosionPower, ImpactPoint, ExplosionRadius);

            collision.rigidbody.AddForceAtPosition(ExplosionVector, ImpactPoint);

            gameObject.SetActive(false);
        }
    }

    public static void AddExplosionForce(Rigidbody2D Body, float Force, Vector3 Position, float Radius)
    {
        var Direction = (Body.transform.position - Position);
        float Calc = 1 - (Direction.magnitude / Radius);
        if (Calc <= 0)
        {
            Calc = 0;
        }
        Body.AddForce(Direction.normalized * Force * Calc);
    }
}
