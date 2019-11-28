using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    //Transforms
    public Transform planet;
    public Transform shield;
    //Vectors
    Vector2 dir;
    public Vector3 plDirfromPlayer;
    public Vector3 directionOfplayerFromPlanet;
    //floats
    public float gravitationalForce = 100;
    float JumpVelocity; 
    public float speed = 14;
    public float input;
    public float time;
    private float initailTime;
    private float deg;
    public float testval1;
    //int
    public static float energy = 100;

    //bool
        //sumar óþarfa bool breytur fyrir testing
    public bool istime;
    public bool isjumping;
    public bool inair;
    public static bool shieldActive = true;
        //bool array til að gá hvort energy má endurnýja sig
    private bool[] allowed = new bool[2];
    
    void Start()
    {   
        for (int i = 0; i < allowed.Length; i++)
        {
            allowed[i] = true;
        }
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
        //svo player labbi ekki af plánetu
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);
        
        if(Input.GetKeyDown(KeyCode.Space)){
            istime = true;
            isjumping = true;
            
        }
        if(deg < 20 && deg < 340)
        deg -= 360;
        //print(deg);
        Jump();
        //ef timer er minni en 0.1 sek er inair bool stillt á true, þetta er gerr svo að fly 
        //fallið keyri ekki strax og ýtt er á space oh leyfir því player að hoppa áður en er flogið
        if(time<(0.1f)){
            inair = true;
        }
        Fly();
        Shieldd();
        if(Input.GetKeyDown(KeyCode.Q)){
            Application.Quit();
        }
        
        
        
    }
    

    void FixedUpdate()
    {
        if(allowed.All(x => x))energy += 1;
        energy=Clamp(energy, 0, 100);
        //Fær staðsetningu á player miðað við plánetu og ýtir svo player að plánetu
        directionOfplayerFromPlanet = (planet.position-transform.position).normalized;
        rb.AddForce (directionOfplayerFromPlanet*gravitationalForce);   
        deg = FindDegree(transform.position.x,transform.position.y);
        //fær 1 eða -1 
        input = Input.GetAxisRaw("Horizontal");
        //labbar til vinstri eða hægri
        rb.velocity = input * transform.right * speed;
        
    }
    void LateUpdate()
    {
        // * Time.deltaTime;
    } 
    void Jump(){
        
        if(istime)
        {
            //þar sem að player er með stöðugt artificial gravity sem miðast við plánetu að þegar player á að hoppa
            //er hægt að bara breyta gildinu á gravity í mínus  
            time-=Time.deltaTime;
            gravitationalForce=-500;
            //þegar tíminn er hálfnaður er gravity stillt á há plús tölu svo hann fari hratt niður 
            if(time < initailTime*0.5)gravitationalForce=500;
            //og eftir að player snertir plánetu er gravity stillt aftur á 100
            //if(time<0){istime=false;}
        }
    }
    void Fly(){
        if(inair)
        {   
            if(Input.GetKey(KeyCode.Space)){energy-=1;
                allowed[0] = false;}

            if(Input.GetButton("Jump")&&energy>1){
                //gravity stillt á mínus 200 á meðan player er í lofti og space er haldið inni
                gravitationalForce=-200;
                
            }
            else{
                //ef space er sleppt dettur player niður með gravity í 400, en umleið og hann snertið plánetu er gravity stillt á 100
                gravitationalForce=400;
                allowed[0]=true;
            }
        }
        //else energy+=1;
    }

    void Shieldd(){
        if(Input.GetKey(KeyCode.LeftShift)&&energy>0){
            shield.gameObject.SetActive(true);
            shieldActive = true;
            energy-=1;
            allowed[1]=false;
        }
        else{
            shield.gameObject.SetActive(false);
            shieldActive = false;
            //energy+=1;
            allowed[1]=true;
        }
    }

    public static float FindDegree(float x, float y){
     float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
     if(value < 0) value += 360f;
 
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
    public static float Clamp( float value, float min, float max )
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
