      /*Vector3 v3 = player.position - transform.position;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        qTo = Quaternion.AngleAxis (angle, Vector3.forward);
                 //transform.rotation = Quaternion.RotateTowards (transform.rotation, qTo, rotationSpeed * Time.deltaTime);
         transform.Translate (Vector3.right * speed * Time.deltaTime);*/


                //testval3 = Vector2.Distance(player.position, transform.position);
        //if(testval3 < 0.259)print("000");
        //input=(Vector2.Distance(player.position, transform.position)) < 24 ? 1 : -1;
        //input = testval3 < 3 ? 1 : -1;
        //input = enDeg < plDeg ? 1 : -1;
        //print(Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)); 



         //input=(Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y))
        //print(Mathf.Atan2((Mathf.Sin(plDeg)*13f),(Mathf.Cos(plDeg)*13f)));
        //print((Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)));
        //plDeg-=plDeg*2;
        //testval3 = (Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)/360)*(2*Math.PI*0.5f);
        //print ((Find2Degree(transform.position.x, transform.position.y, player.position.x, player.position.y)));
        //print(Vector2.Distance(player.position, transform.position));

            public static float FindDegree(float x, float y)
    {
     float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
     if(value < 0) value += 360f;
 
     return value;
    }
         public static float Find2Degree(float x1, float y1, float x2, float y2){
     float value = (float)(Math.Atan((y1 - y2) / (x1 - x2)) * (180 / Mathf.PI));
         
 
     return value;
 }

         enDeg = FindDegree(transform.position.x,transform.position.y);
        plDeg = FindDegree(player.position.x, player.position.y);