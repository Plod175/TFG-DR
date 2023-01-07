using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public float speed = 2f;

    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void OnEnable()
    {
        r2d = GetComponent<Rigidbody2D>();
        r2d.velocity = Vector2.left * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Destroyer" || c2d.tag == "Player"){
            Destroy(gameObject);
        }
    }
}
