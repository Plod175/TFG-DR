using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnergyController : MonoBehaviour
{
    private Action<EnergyController> releaseAction;
    private float lifeTime =22f;
    public GameObject player;
    private bool isRight;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(TimeUp());
        player = GameObject.FindGameObjectWithTag("Player");
        isRight = player.GetComponent<PlayerController>().right;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight){
            transform.position = transform.position + new Vector3(20 * Time.deltaTime, 0, 0);
        }else{
            transform.position = transform.position - new Vector3(20 * Time.deltaTime, 0, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Enemy"){
            releaseAction(this);
        }
    }

    public void ReleaseEnergyAction(Action<EnergyController> releaseActionParam){
        releaseAction = releaseActionParam;
    }

    private IEnumerator TimeUp(){
        yield return new WaitForSeconds(lifeTime);
        releaseAction(this);
    }
}
