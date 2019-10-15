using UnityEngine;
using System.Collections.Generic;
using System.Collections;
 

public class GravitationalBody : MonoBehaviour {
 


/*public Transform player;
private float gravitationalForce = 100;
private Vector3 directionOfplayerFromPlanet;
void Start ()
{
directionOfplayerFromPlanet = Vector3.zero;
}
 
void FixedUpdate ()
{
directionOfplayerFromPlanet = (transform.position-player.position).normalized;
player.GetComponent<Rigidbody2D>().AddForce (directionOfplayerFromPlanet*gravitationalForce);   

}
}
/*
    public float maxDistance;
    public float startingMass;
    public Vector2 initialVelocity;
    Rigidbody2D rb;
 
    //I use a static list of bodies so that we don't need to Find them every frame
    static List<Rigidbody2D> attractableBodies = new List<Rigidbody2D>();
 
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        SetupRigidbody2D();
        //Add this gravitational body to the list, so that all other gravitational bodies can be effected by it
        attractableBodies.Add (rb);
 
    }
 
    void SetupRigidbody2D() {
 
        rb.gravityScale = 0f;
        rb.drag = 0f;
        rb.angularDrag = 0f;
        rb.mass = startingMass;
        rb.velocity = initialVelocity;
 
    }
 
    void FixedUpdate() {
 
        foreach (Rigidbody2D otherBody in attractableBodies) {
 
            if (otherBody == null)
                continue;
 
            //We arn't going to add a gravitational pull to our own body
            if (otherBody == rb)
                continue;
 
            otherBody.AddForce(DetermineGravitationalForce(otherBody));
 
        }
 
    }
 
    Vector2 DetermineGravitationalForce(Rigidbody2D otherBody) {
 
        Vector2 relativePosition = rb.position - otherBody.position;
   
        float distance = Mathf.Clamp (relativePosition.magnitude, 0, maxDistance);
 
        //the force of gravity will reduce by the distance squared
        float gravityFactor = 1f - (Mathf.Sqrt(distance) / Mathf.Sqrt(maxDistance));
 
        //creates a vector that will force the otherbody toward this body, using the gravity factor times the mass of this body as the magnitude
        Vector2 gravitationalForce = relativePosition.normalized * (gravityFactor * rb.mass);
 
        return gravitationalForce;
       
    }
}
   */
}//dir = new Vector2(transform.up.x, transform.up.y);