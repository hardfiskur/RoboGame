using UnityEngine;
using System.Collections.Generic;
using System.Collections;
 

public class GravitationalBody : MonoBehaviour {
 

public float mass;     		
	// The radius of the "sphere of influence" This can be set to infinity (or a very large number) for more realistic gravity.
	public int soiRadius;	
	// Used to alter (unatuarally) the coorelation between the proximity of the objects to the severity of the attraction.  Tweak to make orbits easier to achieve or more intersting.
	public int proximityModifier = 195;	

	// On init of obj
	void Start() {
		mass = mass * 100000; // Mass ^ 5 in order to allow the relative mass input to be more readable
	}
	
	// Creates a visual representation of the sphere of influence in the editor
	public void OnDrawGizmos() {
		// Show the Object's Sphere Of Influence
		Gizmos.DrawWireSphere (transform.position, soiRadius);
	}
	
	void FixedUpdate () { // Runs continuously during gameplay

		// Get all objects that will be affected by gravity (Game objects are tagged in order to be influenced by gravity)
		GameObject[] objectsAffectedByGravity;
        objectsAffectedByGravity = GameObject.FindGameObjectsWithTag ("affectedByPlanetGravity");
		
		foreach (GameObject gravBody in objectsAffectedByGravity) { // Iterate through objects affected by gravity
				
			Rigidbody2D gravRigidBody = gravBody.GetComponent<Rigidbody2D> (); // Get the object's Rigid Body Component
			
			float orbitalDistance = Vector3.Distance (transform.position, gravRigidBody.transform.position); // Get the object's distance from the World Body
			
			if (orbitalDistance < soiRadius) { // If the object is in the sphere of influence (close enough to be affected by the gravity of this object)

				// Get info about the object in the sphere of influence

				Vector3 objectOffset = transform.position - gravRigidBody.transform.position; // Get the object's 2d offset relative to this World Body
				objectOffset.z = 0;
				
				Vector3 objectTrajectory = gravRigidBody.velocity; // Get object's trajectory vector
				
				float angle = Vector3.Angle (objectOffset, objectTrajectory); // Calculate object's angle of attack ( Not used here, but potentially insteresting to have )
				
				float magsqr = objectOffset.sqrMagnitude; // Square Magnitude of the object's offset
				
				if ( magsqr > 0.0001f ) { // If object's force is significant

					// Apply gravitational force to the object
					Vector3 gravityVector = ( mass * objectOffset.normalized / magsqr ) * gravRigidBody.mass;
					gravRigidBody.AddForce ( gravityVector * ( orbitalDistance/proximityModifier) );
					
				}
			} 
		}
	}





/*public Transform player;
private float gravitationalForce = 10;
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
       
    }*/
   
}