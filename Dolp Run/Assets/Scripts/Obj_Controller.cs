using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Obj_Controller : MonoBehaviour
{
    private float parallaxSpeed = 1.2f;
    private float lifeTime =22f;
    private Action<Obj_Controller> releaseAction;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(TimeUp());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - new Vector3(parallaxSpeed * Time.deltaTime, 0, 0);
    }

    private IEnumerator TimeUp(){
        yield return new WaitForSeconds(lifeTime);
        releaseAction(this);
    }

    public void ReleaseObjAction(Action<Obj_Controller> releaseActionParam){
        releaseAction = releaseActionParam;
    }
}
