using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    //Transforms
    public Transform planet;
    public Transform player; 
    //Vectors
    Vector2 dir;
    public Vector3 plDirfromPlayer;
    public Vector3 directionOfplayerFromPlanet;
    public Vector3 playerEnDir;
    //floats
    public float gravitationalForce = 100;
    float JumpVelocity; 
    private float speed = 2;
    public float input;
    public float time;
    private float initailTime;

    public float testval1;
    public float testval2;
    public float testval3;
    //bool
        //sumar óþarfa bool breytur fyrir testing
    public bool istime;
    public bool isjumping;
    public bool inair;
    
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
        //fær áttina sem player er frá plánetu
        plDirfromPlayer = transform.position - planet.position;
        playerEnDir = (transform.position - player.position).normalized;
        //svo player labbi ekki af plánetu
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
        
    }
    

    void FixedUpdate()
    {
        //Fær staðsetningu á player miðað við plánetu og ýtir svo player að plánetu
        directionOfplayerFromPlanet = (planet.position-transform.position).normalized;
        rb.AddForce (directionOfplayerFromPlanet*gravitationalForce);   

        //fær 1 eða -1 
        input = transform.position < player.position ? 1 : -1;//follow
        testval1 = playerEnDir.x - playerEnDir.y;
        testval2 = playerEnDir.x * playerEnDir.y;
        testval3 = testval1+testval2;
        //labbar til vinstri eða hægri
        rb.velocity = input * transform.right * speed;
        
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
}
