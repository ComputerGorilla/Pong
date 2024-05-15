using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHController : MonoBehaviour
{
    public Transform myTrans;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        myTrans = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTrans.position += new Vector3(0, Input.GetAxis("Horizontal")*Time.deltaTime * speed ,0);
        myTrans.position = new Vector3(myTrans.position.x, Mathf.Clamp(myTrans.position.y, -11.5f, 11.5f), 0);
    }
}
