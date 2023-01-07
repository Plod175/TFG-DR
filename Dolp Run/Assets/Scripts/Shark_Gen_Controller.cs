using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shark_Gen_Controller : MonoBehaviour
{
    [SerializeField] private Shark_Controller sharkPrefab;
    [SerializeField] private Transform SharkGen;
    [SerializeField] private float spawnTime;


    [SerializeField] private Transform[] SpawnPoints;
    private int randomNumber;
    
    private ObjectPool<Shark_Controller> sharkPool;
    // Start is called before the first frame update
    void Start()
    {
        sharkPool = new ObjectPool<Shark_Controller>(() => {
            Shark_Controller shark = Instantiate(sharkPrefab, transform.position, Quaternion.identity);
            shark.ReleaseSharkAction(ReleaseShark);
            return shark;
        }, shark =>{
            shark.transform.position = SharkGen.position;
            shark.gameObject.SetActive(true);
        }, shark =>{
            shark.gameObject.SetActive(false);
        }, shark =>{
            Destroy(shark.gameObject);
        }, true, 10, 20);

        InvokeRepeating("RandomSpawn", 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReleaseShark(Shark_Controller shark){
        sharkPool.Release(shark);
    }

    private void spawnShark(){

        sharkPool.Get();
    }

    public void StartGen(){
        InvokeRepeating("spawnShark", 0f, spawnTime);
    }

    public void CacelGen(bool clean = false){
        CancelInvoke("spawnShark");
        if(clean){
            Object[] allObj = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject obstacle in allObj)
            {
                Destroy(obstacle);
            }
        }
    }

    private void RandomSpawn(){
        transform.position = SpawnPoints[randomNumber].position;
        randomNumber = Random.Range(0, SpawnPoints.Length);
    }
}
