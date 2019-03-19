using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Phase { First, Second, Final }

    public GameObject GrenadeObject;
    public GameObject BoxObject;
    public Transform LaunchPoint;
    public float DefaultVerticalSpeed = 15;
    public float DefaultHorizontalSpeed = 15;
    public float BoxTimerMax;
    public float GrenadeTimerMax;
    public int MaxHealth = 10;

    private float BoxTimer;
    private float GrenadeTimer;
    private float VerticalSpeed;
    private float HorizontalSpeed;
    private int Health;
    private Vector3 StageDimensions;
    private float ScreenHeight;
    private bool MovingUp = true;
    private Rigidbody2D MyRigidBody;
    private CircleCollider2D MyCollider;
    private SpriteRenderer MySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SetPhase(Phase.First);

        MyRigidBody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<CircleCollider2D>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();

        Health = MaxHealth;
        BoxTimer = BoxTimerMax;
        GrenadeTimer = GrenadeTimerMax;

        StageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        MyRigidBody.velocity = new Vector2(HorizontalSpeed, VerticalSpeed);

        if (transform.position.y > StageDimensions.y - MySpriteRenderer.bounds.extents.y)
        {
            if (MovingUp)
            {
                MovingUp = false;
                ChangeVerticalDirection();
            }
        }

        BoxTimer -= Time.deltaTime;

        if (BoxTimer <= 0)
        {
            BoxTimer = BoxTimerMax;
            SpawnBox();
        }

        GrenadeTimer -= Time.deltaTime;

        if (GrenadeTimer <= 0)
        {
            GrenadeTimer = GrenadeTimerMax;
            SpawnGrenade();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TakeDamage();
        }
    }

    void ChangeVerticalDirection()
    {
        VerticalSpeed = -VerticalSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!MovingUp)
            {
                MovingUp = true;
                ChangeVerticalDirection();          
            }        
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            //TODO Deal Damage to Player    
            Debug.Log("Enemy hit by bullet");

            Destroy(collision.gameObject);

            TakeDamage();
        }
    }

    void SpawnBox()
    {
        Debug.Log("Spawn box");
        // Box SpawnedBox = Instantiate(BoxObject, LaunchPoint.position, Quaternion.identity)?.GetComponent<Box>();
        //Instantiate(BoxObject, LaunchPoint, true); 
        GameObject SpawnedBox = TheCoolerObjectPooler.SharedInstance.GetPooledObject("Box");

        if (SpawnedBox != null)
        { 
            SpawnedBox.transform.position = LaunchPoint.transform.position;
            SpawnedBox.transform.rotation = LaunchPoint.transform.rotation;
            SpawnedBox.SetActive(true);           
        }
    }

    void SpawnGrenade()
    {
        //Grenade SpawnedGrenade = Instantiate(GrenadeObject, LaunchPoint.position, Quaternion.identity)?.GetComponent<Grenade>();
        //Instantiate(GrenadeObject, LaunchPoint, true);

        GameObject SpawnedGrenade = TheCoolerObjectPooler.SharedInstance.GetPooledObject("Grenade");
        if (SpawnedGrenade != null)
        {
            SpawnedGrenade.transform.position = LaunchPoint.transform.position;
            SpawnedGrenade.transform.rotation = LaunchPoint.transform.rotation;
            SpawnedGrenade.SetActive(true);
        }
    }

    void TakeDamage()
    {
        Health -= 1;

        if (Health <= MaxHealth/2)
        {
            SetPhase(Phase.Second);

        }

      else if (Health <= MaxHealth/3)
        {
            SetPhase(Phase.Final);
        }
        
    }

    void SetPhase(Phase CurrentPhase)
    { 
            switch (CurrentPhase)
            {
            case Phase.First:
                Debug.Log("First Phase");
                VerticalSpeed = DefaultVerticalSpeed;
                HorizontalSpeed = DefaultHorizontalSpeed;
                    break;
                case Phase.Second:
                VerticalSpeed = (DefaultVerticalSpeed * 1.2f);
                HorizontalSpeed = (DefaultHorizontalSpeed * 1.2f);
                BoxTimerMax = (BoxTimerMax * 0.5f);
                Debug.Log("Second Phase");
                    break;
                case Phase.Final:
                VerticalSpeed = (DefaultVerticalSpeed * 1.5f);
                HorizontalSpeed = (DefaultHorizontalSpeed * 1.5f);
                BoxTimerMax = (BoxTimerMax * 0.5f);
                GrenadeTimerMax = (GrenadeTimerMax * 0.7f);
                Debug.Log("Final Phase");
                    break;
                default:
                VerticalSpeed = DefaultVerticalSpeed;
                HorizontalSpeed = DefaultHorizontalSpeed;
                Debug.Log("Unknown Phase");
                    break;
            }
    }
}
