using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundary : MonoBehaviour
{

	public enum BoundaryLocation
    { 
		LEFT, TOP, RIGHT, BOTTOM
	};

	 public BoundaryLocation Direction;
	 private BoxCollider2D Barrier;
     public float BoundaryWidth = 0.8f;
     public float Overhang = 1.0f; // Add extra length to avoid gaps
                         
	void Start ()
    {

		// Get the the world coordinates of the corners of the camera viewport.

		Vector3 TopLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
		Vector3 TopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
		Vector3 LowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 LowerRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0));

		// Get this game objects BoxCollider2D

		Barrier = GetComponent<BoxCollider2D>();

		// Depending on the assigned 'direction' of the Boundary adjust the size and position based on the camera viewport 

		if (Direction == BoundaryLocation.TOP)
        {
      Barrier.size = new Vector2(Mathf.Abs(TopLeft.x) + Mathf.Abs(TopRight.x) + Overhang, BoundaryWidth);
      Barrier.offset = new Vector2(0, BoundaryWidth/2);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 1)) ;
		}	
		if (Direction == BoundaryLocation.BOTTOM)
        {
      Barrier.size = new Vector2(Mathf.Abs(TopLeft.x) + Mathf.Abs(TopRight.x) + Overhang, BoundaryWidth);
      Barrier.offset = new Vector2(0, -BoundaryWidth/2);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 1)) ;
		}
		if (Direction == BoundaryLocation.LEFT)
        {
      Barrier.size = new Vector2(BoundaryWidth, Mathf.Abs(LowerLeft.y) + Mathf.Abs(LowerRight.y) + Overhang);
      Barrier.offset = new Vector2(-BoundaryWidth/2, 0);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2, 1)) ;
		}
		if (Direction == BoundaryLocation.RIGHT)
        {
      Barrier.size = new Vector2(BoundaryWidth, Mathf.Abs(LowerLeft.y) + Mathf.Abs(LowerRight.y) + Overhang);
      Barrier.offset = new Vector2(BoundaryWidth/2, 0);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, 1)) ;
		}
	}


    //Move borders with viewport
    private void Update()
    {
        if (Direction == BoundaryLocation.TOP)
        {       
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 1));
        }
        if (Direction == BoundaryLocation.BOTTOM)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 1));
        }
        if (Direction == BoundaryLocation.LEFT)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2, 1));
        }
        if (Direction == BoundaryLocation.RIGHT)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, 1));
        }
    }
}
