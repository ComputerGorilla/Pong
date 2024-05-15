using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    public Transform bola;
    public Transform myTrans;
    public float speed;
    BolaController bolaControllerComp;
    // Start is called before the first frame update
    void Start()
    {
        bola = GameObject.FindGameObjectWithTag("Ball").transform;
        myTrans = gameObject.transform;
        bolaControllerComp = GameObject.FindGameObjectWithTag("Ball").GetComponent<BolaController>();
    }

    // Update is called once per frame
    void Update()
    {
        float myspeed;
        float deltaYpos = bola.position.y - myTrans.position.y;

        if(bolaControllerComp.direction.x < 0){
            if(Mathf.Abs(deltaYpos) < 3){
                if(Mathf.Abs(deltaYpos) < 1){
                    myspeed = speed * 0.1f;
                }
                else{
                    myspeed = speed * 0.25f;
                }
            }
            else{
                myspeed = speed;
            }
        }
        else{
            myspeed = speed * 0.25f;
        }
        
        myTrans.position += new Vector3(0, Mathf.Sign(deltaYpos) * myspeed * Time.deltaTime, 0);
        myTrans.position = new Vector3(myTrans.position.x, Mathf.Clamp(myTrans.position.y, -11.5f, 11.5f), 0);
    }
}
