using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bubble_Gen_Controller : MonoBehaviour
{
    [SerializeField] private Transform BubbleGen;
    [SerializeField] private BubbleController[] BubblePrefabs;
    private float spawnTime = 3;
    private int index = 0;
    private float xRange = 0f;

    private ObjectPool<BubbleController> BubblePool;
    private GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        BubblePool = new ObjectPool<BubbleController>(() => {
            BubbleController bubble = Instantiate(BubblePrefabs[index], transform.position, Quaternion.identity);
            bubble.ReleaseBubbleAction(ReleaseBubble);
            return bubble;
        }, bubble =>{
            bubble.transform.position = BubbleGen.position;
            bubble.gameObject.SetActive(true);
        }, bubble =>{
            bubble.gameObject.SetActive(false);
        }, bubble =>{
            Destroy(bubble.gameObject);
        }, true, 10, 20);

        InvokeRepeating("RandomSpawn", 1f, 1f);
        InvokeRepeating("ResetSpawn", 12f, 12f);
        game = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReleaseBubble(BubbleController bubble){
        BubblePool.Release(bubble);
    }

    private void spawnBubble(){
        if(index == 4){
            index = 0;
        }
        BubblePool.Get();
        index +=1;
    }

    public void StartGen(){
        InvokeRepeating("spawnBubble", 0f, spawnTime);
    }

    public void CacelGen(){
        CancelInvoke("spawnBubble");
        CancelInvoke("ResetSpawn");
    }

    private void RandomSpawn(){
        xRange = Random.Range(-9f, 9f);
        transform.position = new Vector2(xRange, -6f);
        spawnTime = Random.Range(1, 6);
    }

    private void ResetSpawn(){
        if(game.GetComponent<GameController>().gameState == GameState.Playing){
            CancelInvoke("spawnBubble");
            InvokeRepeating("spawnBubble", 0f, spawnTime);
        }
    }
}
