using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
public Transform player;
Vector3 playerToMouseDir;
 public float armLength = 0.8f;
 void Start() {
     // if the sword is child object, this is the transform of the character (or player)
     player = transform.parent.transform;
 }
 void FixedUpdate() {
     // Get the direction between the player and mouse (aka the target position)
     playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
     playerToMouseDir.z = 0; 
     transform.position = player.position + (armLength * playerToMouseDir.normalized);
 }
}