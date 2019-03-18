using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenrator : MonoBehaviour
{

    public GameObject thePlate;
    public Transform generationPoint;
    public float distanceBetween;

    public ObjectPooler theObjectPool;

    private float platformWidth;



    // Start is called before the first frame update
    void Start()
    {
        platformWidth = thePlate.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.y);
            //Instantiate(thePlate, transform.position, transform.rotation);

            GameObject newPlatform = theObjectPool.GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }

    }
}
