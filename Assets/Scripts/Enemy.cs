using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Phase { First, Second, Final }

    public GameObject GrenadeObject;
    public GameObject BoxObject;
    public Transform LaunchPoint;
    public float DefaultVerticalSpeed = 15.0f;
    public float DefaultHorizontalSpeed = 15.0f;
    public float BoxTimerMax;
    public float GrenadeTimerMax;
    public int MaxHealth = 10;

    private Phase CurrentPhase;
    private Color DamageColor = Color.red;
    private float BoxTimer;
    private float GrenadeTimer;
    private float BlinkTimer;
    private float BlinkTimerMax = 0.1f;
    private bool Blinking = false;
    private int BlinkCounter = 0;
    private float VerticalSpeed;
    private float HorizontalSpeed;
    private int Health;
    private Vector3 StageDimensions;
    private float ScreenHeight;
    private bool MovingUp = true;
    private Rigidbody2D MyRigidBody;
    private CapsuleCollider2D MyCollider;
    private MeshRenderer MyMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPhase = Phase.First;
        SetPhase();

        MyRigidBody = GetComponent<Rigidbody2D>();
        MyCollider = GetComponent<CapsuleCollider2D>();
        MyMeshRenderer = GetComponent<MeshRenderer>();

        Health = MaxHealth;
        BoxTimer = BoxTimerMax;
        GrenadeTimer = GrenadeTimerMax;

        StageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        MyRigidBody.velocity = new Vector2(HorizontalSpeed, VerticalSpeed);

        if (transform.position.y > StageDimensions.y -  MyMeshRenderer.bounds.extents.y)
        {
            if (MovingUp)
            {
                MovingUp = false;
                ChangeVerticalDirection();
            }
        }

        //BoxTimer -= Time.deltaTime;

        //if (BoxTimer <= 0)
        //{
        //    BoxTimer = BoxTimerMax;
        //    SpawnBox();
        //}

        GrenadeTimer -= Time.deltaTime;

        if (GrenadeTimer <= 0)
        {
            GrenadeTimer = GrenadeTimerMax;
            SpawnGrenade();
        }

        BlinkTimer -= Time.deltaTime;

        if (BlinkTimer <= 0 && Blinking)
        {
            Blink();
        }

        if (Input.GetKeyDown(KeyCode.Delete))
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
        // Box SpawnedBox = Instantiate(BoxObject, LaunchPoint.position, Quaternion.identity)?.GetComponent<Box>();
        //Instantiate(BoxObject, LaunchPoint, true); 
        GameObject SpawnedBox = TheCoolerObjectPooler.SharedInstance.GetPooledObject("Block");

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

        if (Health <= MaxHealth / 3)
        {
            CurrentPhase = Phase.Final;
            SetPhase();
        }

       else if (Health <= MaxHealth/2)
        {
            CurrentPhase = Phase.Second;
            SetPhase();
        }

        Blinking = true;
        BlinkCounter = 0;
        Blink();             
    }

    void Blink()
    {
      if (BlinkCounter < 6)
        {
            BlinkCounter += 1;
            if (MyMeshRenderer.isVisible)
            {
                MyMeshRenderer.enabled = false;
            }
            else
            {
                MyMeshRenderer.enabled = true;
            }
            BlinkTimer = BlinkTimerMax;

            if (BlinkCounter >= 6)
            {
                Blinking = false;
                MyMeshRenderer.enabled = true;
            }            
        }     
    }

    void SetPhase()
    {
            switch (CurrentPhase)
            {
            case Phase.First:
                Debug.Log("First Phase");
                VerticalSpeed = DefaultVerticalSpeed;
                HorizontalSpeed = DefaultHorizontalSpeed;
                    break;
                case Phase.Second:
                VerticalSpeed = DefaultVerticalSpeed * 1.2f;
                HorizontalSpeed = DefaultHorizontalSpeed * 1.2f;
                BoxTimerMax = BoxTimerMax * 0.7f;
                GrenadeTimerMax = GrenadeTimerMax * 0.7f;
                Debug.Log("Second Phase");
                    break;
                case Phase.Final:
                VerticalSpeed = DefaultVerticalSpeed * 1.5f;
                HorizontalSpeed = DefaultHorizontalSpeed * 1.5f;
                BoxTimerMax = BoxTimerMax * 0.5f;
                GrenadeTimerMax = GrenadeTimerMax * 0.5f;
                Debug.Log("Final Phase");
                    break;
                default:
                VerticalSpeed = DefaultVerticalSpeed;
                HorizontalSpeed = DefaultHorizontalSpeed;
                Debug.Log("Unknown Phase");
                    break;
            }

        if (!MovingUp)
        {
            VerticalSpeed = -VerticalSpeed;
        }
    }
}
