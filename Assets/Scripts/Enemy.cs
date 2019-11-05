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
    private float speed = 5;
    public float input;
    public float time;
    private float initailTime;

    public float p1;
    public float e1;
    public double testval3;
    private float plDeg;
    private float enDeg;
    //bool
        //sumar óþarfa bool breytur fyrir testing
    public bool istime;
    public bool isjumping;
    public bool inair;

    public int pm = 1;


    //TEST
    private Quaternion qTo;
    //TEST
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=0;
        //stilla timer í byrjun
        ResetTimer();
        //auðveldar stillingu timer
        initailTime = time;
    }


    void Update()
    {
        /*Vector3 v3 = player.position - transform.position;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        qTo = Quaternion.AngleAxis (angle, Vector3.forward);
                 //transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
         transform.Translate (Vector3.right * speed * Time.deltaTime);*/


        //fær áttina sem player er frá plánetu
        plDirfromPlayer = transform.position - planet.position;
        playerEnDir = (transform.position - player.position).normalized;
        //svo player labbi ekki af plánetu
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
        /*if(plDeg > 1 && plDeg < 359){
        input = enDeg < plDeg ? 1 : -1;}*/
        print(testval3);
        //input = Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y) < plDeg ? 1 : -1;
        
    }
    

    void FixedUpdate()
    {
        //Fær staðsetningu á player miðað við plánetu og ýtir svo player að plánetu
        directionOfplayerFromPlanet = (planet.position-transform.position).normalized;
        rb.AddForce (directionOfplayerFromPlanet*gravitationalForce);   
        enDeg = FindDegree(transform.position.x,transform.position.y);
        plDeg = FindDegree(player.position.x, player.position.y);
        //fær 1 eða -1 
        /*if(transform.rotation.z > 0 && transform.rotation.z < 180){
            input = transform.rotation.z > player.rotation.z ? 1 : -1;
        }
        else{
            input = transform.rotation.z < player.rotation.z ? -1 : 1;
        }*/
        testval3 = Vector2.Distance(player.position, transform.position)*pm;
        if(testval3 < 0.259)print("000");
        //input=(Vector2.Distance(player.position, transform.position)) < 24 ? 1 : -1;
        input = testval3 < 0 ? 1 : -1;
        //print(Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)); 
        e1= (float)(Math.Atan2(transform.position.x,transform.position.y)/Math.PI) *180f;
        p1= player.position.x*player.position.y;
        //plDeg-=plDeg*2;
        //testval3 = (Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)/360)*(2*Math.PI*0.5f);
        //print ((Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)));
        //print(Vector2.Distance(player.position, transform.position));
        //labbar til vinstri eða hægri
        rb.velocity = input * transform.right * speed;
        
    }
    public static float FindDegree(float x, float y)
    {
     float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
     //if(value < 0) value += 360f;
 
     return value;
    }
         public static float Find2Degree(float x1, float y1, float x2, float y2){
     float value = (float)(Math.Atan((y1 - y2) / (x1 - x2)) * (180 / Mathf.PI));
     //if(value < 0) value += 360f;
 
     return value;
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
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Player")
        pm=-1*pm;
    }
}
