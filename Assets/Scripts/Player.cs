using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private float speed = 4;
    public float input;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        // Storing Player Movement
        input = Input.GetAxisRaw("Horizontal");


        // Moving Player
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(0, 5);
        }
    }
}
