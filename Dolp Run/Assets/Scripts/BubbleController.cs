using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleController : MonoBehaviour
{
    public float speed = 3f;
    private float lifeTime =10f;
    private Action<BubbleController> releaseAction;
    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(TimeUp());

        r2d = GetComponent<Rigidbody2D>();
        r2d.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator TimeUp(){
        yield return new WaitForSeconds(lifeTime);
        releaseAction(this);
    }

    public void ReleaseBubbleAction(Action<BubbleController> releaseActionParam){
        releaseAction = releaseActionParam;
    }
}
