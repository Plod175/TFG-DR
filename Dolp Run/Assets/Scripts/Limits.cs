using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limits : MonoBehaviour
{
    private Transform theTransform;
    public Vector2 Hrange;
    public Vector2 Vrange;
    // Start is called before the first frame update
    void LateUpdate()
    {
        theTransform.position = new Vector3 (
            Mathf.Clamp (transform.position.x,Vrange.x,Vrange.y),
            Mathf.Clamp (transform.position.y,Hrange.x,Hrange.y),
            transform.position.z
        );
    }
    
    void Start()
    {
        theTransform = GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
