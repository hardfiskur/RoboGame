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
    public Vector3 dirplanetEnemy;
    public Vector3 playerEnDir;
    //floats
    public float gravitationalForce = 100;
    float JumpVelocity; 
    private int _speed = 5;
    public float input;
    
    private float plDeg;
    private float enDeg;
    private int initialSpeed;

    private bool[] pSlice = new bool[4];
    private bool[] eSlice = new bool[4];

    Vector3 plPos;
    Vector3 enPos;


    //TEST
    Player playerClass;


    public bool tst;
    //TEST
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale=0;
        //stilla timer í byrjun
        initialSpeed = _speed;
    }


    void Update()
    {
        //playerClass = new Player();
        //finna gráður á player og enemy
        enDeg = FindDegree(enPos.x, enPos.y);
        plDeg = FindDegree(plPos.x, plPos.y);
        plDirfromPlayer = transform.position - planet.position;
        playerEnDir = (transform.position - player.position).normalized;
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);  
        //_speed = initialSpeed;
        /*if(playerClass.ShieldStat == false){
            if(tst == true){
                _speed = initialSpeed;
                tst=false;
            }
        }*/
        plPos = player.position;
        enPos = transform.position;
    }
    

    void FixedUpdate()
    {
        //Gravity
        dirplanetEnemy = (planet.position-transform.position).normalized;
        rb.AddForce (dirplanetEnemy*gravitationalForce);   

        //2 array halda utan um á hvorum hluta tungls er staðið á
        //tunglinu er hægt að skipta á fjóra hluta eftir því hvort x og y er mínus eða plús [(-x,y)(x,y)(x,-y)(-x,-y)]
        Slices(pSlice,plPos);
        Slices(eSlice,enPos);

        //fundið út hvort player er hægra eða vinstra meigin við enemy
        for(int i=0; i<4;i++){
            //ef array[slot + 1] er true þá fer enemy til hægri, sama ef slot - 1 er true nema þá til vinstri
            int k1 = i+1;
            int k2 = i-1;
            k1=k1>3?0:k1;
            k2=k2<0?3:k2;
            if(pSlice[i] && eSlice[k2])input=1;
            else if(pSlice[i] && eSlice[k1])input=-1;

            if(pSlice[i]==eSlice[i])input= enDeg<plDeg?1:-1;
        }

        //
        rb.velocity = input * (_speed * transform.right);
       
    }
    
    void OnTriggerStay2D(Collider2D col){
    if(col.gameObject.tag == "shield"){_speed = 0;}//_speed = 0;tst=true;}
    }
    void OnTriggerExit2D(Collider2D col){
    if(col.gameObject.tag == "shield"){_speed = initialSpeed;}//_speed = 0;tst=true;}
    }

    //reiknar gráðu player/enemy á tungli
    public static float FindDegree(float x, float y){
        float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        if(value < 0) value += 360f;

        return value;
    }

    void LateUpdate()
    {
        if(!Player.shieldActive)_speed = initialSpeed;
    }


    
//uppfærir array
    static void Slices(bool[] arr,Vector3 pos){
        
        arr[0]=pos.x>0 && pos.y<0?true:false;
        arr[1]=pos.x<0 && pos.y<0?true:false;
        arr[2]=pos.x<0 && pos.y>0?true:false;
        arr[3]=pos.x>0 && pos.y>0?true:false;
    }

}


