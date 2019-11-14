using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
private Transform player;
public Vector3 playerToMouseDir;
public float armLength = 0.67f;

//Enemy enemy = new Enemy(); 
 void Start() {
     // if the sword is child object, this is the transform of the character (or player)
     player = transform.parent.transform;
 }
 void FixedUpdate() {
     // Get the direction between the player and mouse (aka the target position)
     playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
     playerToMouseDir.z = 0; 
     transform.position = player.position + (armLength * playerToMouseDir.normalized);
     float angle = Mathf.Atan2(playerToMouseDir.y, playerToMouseDir.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
 }

}