using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin_Gen_Controller : MonoBehaviour
{
    [SerializeField] private CoinController coinPrefab;
    [SerializeField] private BonusController BonusPrefab;
    [SerializeField] private Transform CoinGen;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] SpawnPoints;
    private ObjectPool<CoinController> coinPool;
    private GameObject obj;
    private int randomNumber;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        coinPool = new ObjectPool<CoinController>(() => {
            CoinController coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.ReleaseCoinAction(ReleaseCoin);
            return coin;
        }, coin =>{
            coin.transform.position = CoinGen.position;
            coin.gameObject.SetActive(true);
        }, coin =>{
            coin.gameObject.SetActive(false);
        }, coin =>{
            Destroy(coin.gameObject);
        }, true, 20, 30);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReleaseCoin(CoinController coin){
        coinPool.Release(coin);
    }

    private void spawnCoin(){

        coinPool.Get();
    }

    public void StartGen(){
        InvokeRepeating("spawnCoin", 0f, spawnTime);
        InvokeRepeating("RandomSpawn", 5f, 5f);
        InvokeRepeating("BonusSpawn", 20f, 20f);
    }

    public void CacelGen(bool clean = false){
        CancelInvoke("spawnCoin");
        CancelInvoke("BonusSpawn");
        if(clean){
            obj = GameObject.FindGameObjectWithTag("Bonus");
            Destroy(obj);
            Object[] allObj = GameObject.FindGameObjectsWithTag("Coin");
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

    private void BonusSpawn(){
        if(randomNumber != 0){
            Instantiate(BonusPrefab, SpawnPoints[randomNumber - 1].position, Quaternion.identity);
        }else{
            Instantiate(BonusPrefab, SpawnPoints[2].position, Quaternion.identity);
        }
    }

    public void SoundCoin(){
        sound.Play();
    }
}
