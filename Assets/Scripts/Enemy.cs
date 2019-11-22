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
    public Vector3 dirplanetPlayer;
    public Vector3 dirplanetEnemy;
    public Vector3 playerEnDir;
    //floats
    public float gravitationalForce = 100;
    float JumpVelocity; 
    private int _speed = 5;
    public float input;
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
    public bool pa,pb,pc,pd;
    public bool ea,eb,ec,ed;
    public bool pm;

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
        //print(Mathf.Abs(Find2Degree(enPos.x,enPos.y,plPos.x,plPos.y)));//change name on trigger?
        //AltTrigger(player);
        playerClass = new Player();
        //finna gráður á player og enemy
        enDeg = FindDegree(enPos.x, enPos.y);
        plDeg = FindDegree(plPos.x, plPos.y);
        
        //float diff = (enDeg + plDeg) % 360;
        //fær áttina sem player er frá plánetu
        plDirfromPlayer = transform.position - planet.position;
        playerEnDir = (transform.position - player.position).normalized;
        //svo player labbi ekki af plánetu
        transform.right = Vector3.Cross(plDirfromPlayer, Vector3.forward);  
        //_speed = initialSpeed;
        if(playerClass.ShieldStat == false){
            print("FASDF");
            if(tst == true){
            _speed = initialSpeed;
            tst=false;
        }
        }
        plPos = player.position;
        enPos = transform.position;
        
        
    }
    

    void FixedUpdate()
    {
        

        
        dirplanetPlayer = (planet.position-player.position).normalized;
        dirplanetEnemy = (planet.position-transform.position).normalized;
        rb.AddForce (dirplanetEnemy*gravitationalForce);   
        //pm = (dirplanetEnemy.x+dirplanetEnemy.y) < (dirplanetPlayer.x+dirplanetPlayer.y) ? true : false;
        e1= (float)(Math.Atan2(transform.position.x,transform.position.y)/Math.PI) *180f;
        p1= (float)(Math.Atan2(player.position.x,player.position.y)/Math.PI) *180f;
        //input= e1 < p1 ? 1 : -1;



    //print(Mathf.Abs(WrapAngle(player.localEulerAngles.z)));

        pa=plPos.x>0 && plPos.y<0?true:false;
        pb=plPos.x<0 && plPos.y<0?true:false;
        pc=plPos.x<0 && plPos.y>0?true:false;
        pd=plPos.x>0 && plPos.y>0?true:false;
        pSlice[0]=pa;pSlice[1]=pb;pSlice[2]=pc;pSlice[3]=pd;
        ea=enPos.x>0 && enPos.y<0?true:false;
        eb=enPos.x<0 && enPos.y<0?true:false;
        ec=enPos.x<0 && enPos.y>0?true:false;
        ed=enPos.x>0 && enPos.y>0?true:false;
        eSlice[0]=ea;eSlice[1]=eb;eSlice[2]=ec;eSlice[3]=ed;

        //if(pSlice[])

        
        for(int i=0; i<4;i++){
            int k1 = i+1;
            int k2 = i-1;
            k1=k1>3?0:k1;
            k2=k2<0?3:k2;
            if(pSlice[i] && eSlice[k2])input=1;
            else if(pSlice[i] && eSlice[k1])input=-1;

            if(pSlice[i]==eSlice[i])input= enDeg<plDeg?1:-1;
        }


        rb.velocity = input * (_speed * transform.right);

    }




    void OnTriggerStay2D(Collider2D col){
    if(col.gameObject.tag == "shield"){_speed = 0;}//_speed = 0;tst=true;}
    }
    void OnTriggerExit2D(Collider2D col){
    if(col.gameObject.tag == "shield"){_speed = initialSpeed;}//_speed = 0;tst=true;}
    }


       public static float FindDegree(float x, float y){
     float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
     if(value < 0) value += 360f;
 
     return value;
 }

 void LateUpdate()
 {
     if(player.gameObject.name == "Player")_speed = initialSpeed;
 }
  public static float Find2Degree(float x2, float y2, float x1, float y1){
     float value = (float)(Math.Atan((y1 - y2) / (x1 - x2)) * (180 / Mathf.PI));
         
 
     return value;
 }
 private static float WrapAngle(float angle)
        {
            angle%=360;
            if(angle >180)
                return angle - 360;
 
            return Mathf.Abs(angle);
        }
    
}

