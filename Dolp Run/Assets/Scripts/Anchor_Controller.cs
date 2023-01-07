using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_Controller : MonoBehaviour
{
    private float parallaxSpeed = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 0.5){
            transform.position = transform.position - new Vector3(parallaxSpeed * Time.deltaTime, 0.8f * Time.deltaTime, 0);
        }else{
        transform.position = transform.position - new Vector3(parallaxSpeed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Destroyer"){
            Destroy(gameObject);
        }
    }
}
