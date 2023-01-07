using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinController : MonoBehaviour
{
    public float speed = 2f;
    private Action<CoinController> releaseAction;
    private GameObject controller;

    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void OnEnable()
    {
        r2d = GetComponent<Rigidbody2D>();
        r2d.velocity = Vector2.left * speed;
        controller = GameObject.Find("CoinGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Destroyer"){
            releaseAction(this);
        }else if (c2d.tag == "Player"){
            controller.SendMessage("SoundCoin");
            releaseAction(this);
        }
    }

    public void ReleaseCoinAction(Action<CoinController> releaseActionParam){
        releaseAction = releaseActionParam;
    }

}
