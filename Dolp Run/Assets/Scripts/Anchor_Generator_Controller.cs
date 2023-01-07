using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor_Generator_Controller : MonoBehaviour
{
    private GameObject game;
    [SerializeField] private GameObject anchor;
    [SerializeField] private Transform AnchorGen;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("MainCanvas");
        InvokeRepeating("AnchorSpawn", 10f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AnchorSpawn(){
        if(game.GetComponent<GameController>().gameState == GameState.Playing){
            Instantiate(anchor, AnchorGen.position, Quaternion.identity);
        }
    }


}
