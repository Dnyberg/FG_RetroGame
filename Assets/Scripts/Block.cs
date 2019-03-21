using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block: MonoBehaviour
{
    public float ImpactForce = 50000;
    public float ImpactRadius = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Vector2 ImpactPoint = collision.GetContact(0).point;

            AddImpactForce(GetComponent<Rigidbody2D>(), ImpactForce, ImpactPoint, ImpactRadius);

            //collision.rigidbody.AddForceAtPosition(ExplosionForce, ImpactPoint, ForceMode2D.Impulse);

           // gameObject.SetActive(false);
        }
    }

    public static void AddImpactForce(Rigidbody2D Body, float Force, Vector3 Position, float Radius)
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
