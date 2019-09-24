using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform planet;
    private float rotForce = 10;
    private Vector3 plDirfromPlayer;
    /*private bool allowForce;


    void Start()
    {
        plDirfromPlayer = Vector3.zero;
    }
 
    void Update ()
    {
    
        allowForce = false;

        if (Input.GetKey(KeyCode.Space))
            allowForce = true;
            plDirfromPlayer = transform.position - planet.position;
            transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
    }
    
    void FixedUpdate ()
    {
        if (allowForce)
        rb.AddForce (transform.right * rotForce);
    }*/
Rigidbody2D rb;

    private float speed = 4;
    public float input;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=0f;
    }

    // Update is called once per frame
    void Update(){
        plDirfromPlayer = transform.position - planet.position;
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
    }
    void FixedUpdate()
    {
        // Storing Player Movement
        input = Input.GetAxisRaw("Horizontal");


        // Moving Player
        rb.AddForce (input * transform.right * rotForce);
        /*rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(0, 5);
        }*/
    }
}
