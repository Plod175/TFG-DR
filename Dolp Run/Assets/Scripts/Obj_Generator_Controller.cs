using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Obj_Generator_Controller : MonoBehaviour
{
    [SerializeField] private Transform ObjGen;
    [SerializeField] private Obj_Controller[] ObjPrefabs;
    private float spawnTime = 2;
    private int index = 0;
    private float yRange = 0f;

    private ObjectPool<Obj_Controller> ObjPool;
    private GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        ObjPool = new ObjectPool<Obj_Controller>(() => {
            Obj_Controller Obj = Instantiate(ObjPrefabs[index], transform.position, Quaternion.identity);
            Obj.ReleaseObjAction(ReleaseObj);
            return Obj;
        }, Obj =>{
            Obj.transform.position = ObjGen.position;
            Obj.gameObject.SetActive(true);
        }, Obj =>{
            Obj.gameObject.SetActive(false);
        }, Obj =>{
            Destroy(Obj.gameObject);
        }, true, 10, 20);

        InvokeRepeating("RandomSpawn", 1f, 1f);
        InvokeRepeating("ResetSpawn", 12f, 12f);
        game = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReleaseObj(Obj_Controller Obj){
        ObjPool.Release(Obj);
    }

    private void spawnObj(){
        if(index == 6){
            index = 0;
        }
        ObjPool.Get();
        index +=1;
    }

    public void StartGen(){
        InvokeRepeating("spawnObj", 0f, spawnTime);
    }

    public void CacelGen(bool clean = false){
        CancelInvoke("spawnObj");
        CancelInvoke("ResetSpawn");
        if(clean){
            Object[] allObj = GameObject.FindGameObjectsWithTag("Obj");
            foreach(GameObject groundObj in allObj)
            {
                Destroy(groundObj);
            }
        }
    }

    private void RandomSpawn(){
        yRange = Random.Range(-2.5f, -5.5f);
        transform.position = new Vector2(12, yRange);
        spawnTime = Random.Range(1, 3);
    }

    private void ResetSpawn(){
        if(game.GetComponent<GameController>().gameState == GameState.Playing){
            CancelInvoke("spawnObj");
            InvokeRepeating("spawnObj", 0f, spawnTime);
        }
    }
}
