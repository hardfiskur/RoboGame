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
    
    public bool pm;

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
        float dist = (Vector2.Distance(player.position, transform.position));
        //float dist = Mathf.Sqrt(Math.Abs(Mathf.Pow(2,(enPos.x - plPos.x))-Mathf.Pow(2,(enPos.y-plPos.y))));
        //float theta = Mathf.Acos(1-((Mathf.Pow(2, dist))/((Mathf.Pow(2,(2*25))))));
        //print((Vector2.Distance(player.position, transform.position)*Math.PI));
        //print(diff);
        //print(enDeg+plDeg);

        //print(angle(enDeg, plDeg));
        //180.0 - std::fabs(std::fmod(std::fabs(first - second), 360.0) - 180.0);
        //print(enDeg);
        //print(Math.Abs((Mathf.Abs(enDeg + plDeg) % 360.0) - 180.0));
        bool over = dist > 14.6f ? true : false;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, plDeg, enDeg * Time.deltaTime);
        
        //print(dist+"==="+over);
        //transform.position = Vector2.MoveTowards((transform.position, player.position, _speed * Time.deltaTime));
        //print((transform.position, player.position, _speed * Time.deltaTime));
        
        
    }
    

    void FixedUpdate()
    {

        dirplanetPlayer = (planet.position-player.position).normalized;
        dirplanetEnemy = (planet.position-transform.position).normalized;
        rb.AddForce (dirplanetEnemy*gravitationalForce);   
        pm = (dirplanetEnemy.x+dirplanetEnemy.y) < (dirplanetPlayer.x+dirplanetPlayer.y) ? true : false;
        e1= (float)(Math.Atan2(transform.position.x,transform.position.y)/Math.PI) *180f;
        p1= (float)(Math.Atan2(player.position.x,player.position.y)/Math.PI) *180f;
        //input= e1 < p1 ? 1 : -1;




    input=WrapAngle(transform.localEulerAngles.z)>WrapAngle(player.localEulerAngles.z)?1:-1;
    //eins og stendur fær enemy annað hvort 1 eða -1 í input eftir því hvort gráður eru minni eða meiri.
    //þetta virkar þar til gráður fara frá 0 í 360 eða öfugt. Þá í stað þess að elta player yfir þann punkt fer
    //enemy frekar í öfuga átt í heilan hring þar sem samkvæmt gráðum er það styttra...

    //input = enDeg < plDeg ? 1 : -1;


    //Náði að láta þetta virka næstum fullkomlega! nú þegar enemy og player eru á sama y eða x ás að þá er reiknað út
    //hvort x eða y(öfugt við ás sem enemy og player eru á) á enemy sé meira eða minna en hjá player
             /*if(enPos.y > 0 && plPos.y > 0)input = enPos.x < plPos.x ? 1 : -1;
             
        else if(enPos.y < 0 && plPos.y < 0) input = enPos.x > plPos.x ? 1 : -1;
        //else if(enPos.y < 0 && enPos.x > 0 && plPos.y < 0 && plPos.x > 0)input = enPos.x < plPos.x ? 1 : -1;

        else if(enPos.x > 0 && plPos.x > 0) input = enPos.y > plPos.y ? 1 : -1;
        else if(enPos.x < 0 && plPos.x < 0) input = enPos.y < plPos.y ? 1 : -1;*/

        //depending on which slice player is on. set all other slices 

        rb.velocity = input * (_speed * transform.right);
        /*if(transform.position.y < 0){
            float tX = -1*transform.position.x;
            transform.position = new Vector3(tX,0.5f,0);
        }*/ 
        
        //print(dirplanetPlayer.x+dirplanetPlayer.y);
        //if(player)


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
 
            return angle;
        }
    
}

