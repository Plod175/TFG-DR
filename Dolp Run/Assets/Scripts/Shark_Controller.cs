using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shark_Controller : MonoBehaviour
{
    public float speed = 2f;
    private Action<Shark_Controller> releaseAction;
    public ParticleSystem DestructionEffect;
    private GameObject game;

    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void OnEnable()
    {
        r2d = GetComponent<Rigidbody2D>();
        r2d.velocity = Vector2.left * speed;
        game = GameObject.FindGameObjectWithTag("MainCanvas");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Destroyer"){
            releaseAction(this);
        }else if (c2d.tag == "Energy"){
            Explode();
        }
    }

    public void ReleaseSharkAction(Action<Shark_Controller> releaseActionParam){
        releaseAction = releaseActionParam;
    }

    void Explode()
     {
        ParticleSystem explosionEffect = Instantiate(DestructionEffect) as ParticleSystem;
        var otherPosn = transform.position;
        explosionEffect.transform.position = new Vector3(otherPosn.x, otherPosn.y, -1f);
        explosionEffect.Play();
        game.SendMessage("dieSound");
        Destroy(explosionEffect.gameObject, 5f);
        releaseAction(this);;
     
     }

}
