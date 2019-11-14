using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    //Components
    Rigidbody2D rb;
    //Transforms
    public Transform planet;
    public Transform player; 
    public Transform cheatcol;
    //Vectors
    Vector2 dir;
    public Vector3 plDirfromPlayer;
    public Vector3 directionOfplayerFromPlanet;
    public Vector3 playerEnDir;
    //floats
    public float gravitationalForce = 100;
    float JumpVelocity; 
    private int _speed = 5;
    public float input;
    public float time;
    private float initailTime;
    
    public float p1;
    public float e1;
    public double testval3;
    private float plDeg;
    private float enDeg;
    private int initialSpeed;
    //bool
        //sumar óþarfa bool breytur fyrir testing
    public bool istime;
    public bool isjumping;
    public bool inair;
    
    public int pm = 1;


    //TEST
    Player playerClass = new Player();
    //TEST
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=0;
        //stilla timer í byrjun
        ResetTimer();
        //auðveldar stillingu timer
        initailTime = time;
        initialSpeed = _speed;
    }


    void Update()
    {
        //fær áttina sem player er frá plánetu
        plDirfromPlayer = transform.position - planet.position;
        playerEnDir = (transform.position - player.position).normalized;
        //svo player labbi ekki af plánetu
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);  
        //_speed = initialSpeed;
        if(playerClass.ShieldStat == false){
            _speed = initialSpeed;
        }
        
        
    }
    void LateUpadte()
    {
        _speed = initialSpeed;
    }
    

    void FixedUpdate()
    {

        directionOfplayerFromPlanet = (planet.position-transform.position).normalized;
        rb.AddForce (directionOfplayerFromPlanet*gravitationalForce);   

        e1= (float)(Math.Atan2(transform.position.x,transform.position.y)/Math.PI) *180f;
        p1= (float)(Math.Atan2(player.position.x,player.position.y)/Math.PI) *180f;
        input= e1 < p1 ? 1 : -1;
       
        rb.velocity = input * (_speed * transform.right);
        if(transform.position.y < 0){
            float tX = -1*transform.position.x;
            transform.position = new Vector3(tX,0.5f,0);
        }  
            
            //print(_speed);
    }




    //endurstilla timer
    private void ResetTimer(){
        time = 0.6f;
    }
    private void Defaults(){
        istime=false; 
        inair=false;
        gravitationalForce=100;
    }

    void OnCollisionEnter2D(Collision2D col){
        //þegar player snertir plánetu er timer endurstilltur
        if(col.gameObject.tag == "planet"){ResetTimer();Defaults();}
    }
    void OnTriggerStay2D(Collider2D col){
    if(col.gameObject.tag == "shield"){_speed = 0;}
    }
    
}

