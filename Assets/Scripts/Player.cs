using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 dir;
    public Transform planet;
    private float rotForce = 4;
    private Vector3 plDirfromPlayer;
    Rigidbody2D rb;
    float JumpVelocity;
    float JumpDampening=0.1f;   
    private float speed = 5;
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
        //transform.up = Vector3.Cross(plDirfromPlayer, Vector3.up);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(8,rb.velocity.y);
        // Storing Player Movement
        input = Input.GetAxisRaw("Horizontal");

        dir = new Vector2(transform.up.x, transform.up.y);
        // Moving Player
        //rb.AddForce (input * transform.right * rotForce);
        //if(input!=0)transform.position =  (input * transform.right * rotForce);
        rb.velocity = input * transform.right * speed;
        //rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
    }
    void Jump(){
        
        //dir.y-=9.8f*Time.deltaTime;
        //rb.velocity=dir*55;
        //rb.velocity = dir*35;
        //rb.AddForce(dir*3500, ForceMode2D.Force);
        transform.Translate(0, Time.deltaTime, 0, Space.World);
        print(dir.y);
    }
}
