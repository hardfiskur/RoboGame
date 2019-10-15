using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    //Transforms
    public Transform planet;
    //Vectors
    Vector2 dir;
    private Vector3 plDirfromPlayer;
    private Vector3 directionOfplayerFromPlanet;
    //floats
    private float gravitationalForce = 100;
    float JumpVelocity; 
    private float speed = 5;
    public float input;
    public float time;
    private float initailTime;
    //bool
    public bool istime;
    public bool isjumping;
    public bool inair;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=0;
        ResetTimer();
        initailTime = time;
    }


    void Update()
    {
        plDirfromPlayer = transform.position - planet.position;
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
        
        if(Input.GetKeyDown(KeyCode.Space)){
            istime = true;
            isjumping=true;
            
        }
        //stops looping jump assoon as inair is true, hence not able to jump :I
        Jump();
        //if(inair){Fly();}
    }
    

    void FixedUpdate()
    {
        
        directionOfplayerFromPlanet = (planet.position-transform.position).normalized;
        rb.AddForce (directionOfplayerFromPlanet*gravitationalForce);   
        input = Input.GetAxisRaw("Horizontal");

        dir = new Vector2(transform.up.x, transform.up.y);
        rb.velocity = input * transform.right * speed;
        
    }
    void Jump(){
        
        if(istime)
        {
            time-=Time.deltaTime;
            gravitationalForce=-500;
            inair=true;
            if(time < initailTime*0.5)gravitationalForce=500;
            if(time < 0){gravitationalForce=100;
            istime=false;}
        }
    }
    void Fly(){
        if(Input.GetButton("Fire1")){
            gravitationalForce=-200;
        }
        else{
            gravitationalForce=100;
        }
    }
    private void ResetTimer(){
        time = 0.6f;
    }
    private void ResetFlyTimer(){

    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "planet"){ResetTimer();istime=false; inair=false;}
    }
}
