using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform planet;
    private float rotForce = 4;
    private Vector3 plDirfromPlayer;
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
        //transform.up = Vector3.Cross(plDirfromPlayer, Vector3.up);
    }
    void FixedUpdate()
    {

        // Storing Player Movement
        input = Input.GetAxisRaw("Horizontal");


        // Moving Player
        //rb.AddForce (input * transform.right * rotForce);
        //if(input!=0)transform.position =  (input * transform.right * rotForce);
        rb.velocity = input * transform.right * speed;
        //rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space)){
            //transform.GetComponent<Rigidbody2D>().AddForce(plDirfromPlayer*12,ForceMode2D.Impulse);
            rb.AddForce(transform.up*1500);
            //rb.velocity = transform.up*10;
        }
    }
}
