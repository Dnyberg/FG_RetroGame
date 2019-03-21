using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public bool ShouldDeactivate = false;

  // Destroy or deactivate after leaving boundary
  void OnTriggerExit2D(Collider2D other)
    {
	  if (other.gameObject.tag == "Boundary")
        {
         if (ShouldDeactivate)
         {
             gameObject.SetActive(false);
         }
         else
          {
            Destroy(gameObject);
          }
        }
     }
}

