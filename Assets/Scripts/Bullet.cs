using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public float deathTime = 2f;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
        Destroy(gameObject,deathTime);
    }

    /*public void PlaceCenterAboveObject(Bounds bounds)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector2 position = new Vector2(bounds.center.x, bounds.max.y + renderer.bounds.extents.y);
        body.position = position;
    }
    */
    public void Shoot()
    {
        body.velocity = Vector2.right * speed;
    }
}
