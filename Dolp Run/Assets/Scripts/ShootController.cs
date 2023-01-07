using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootController : MonoBehaviour
{
    private ObjectPool<EnergyController> energyPool;
    private GameObject player;
    [SerializeField] private EnergyController EnergyPrefab;
    [SerializeField] private Transform energyGen;
    private AudioSource sound;
    public AudioClip shootSound;
    private bool canFire = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sound = GetComponent<AudioSource>();

        energyPool = new ObjectPool<EnergyController>(() => {
            EnergyController energy = Instantiate(EnergyPrefab, transform.position, energyGen.rotation);
            energy.ReleaseEnergyAction(ReleaseEnergy);
            return energy;
        }, energy =>{
            energy.transform.position = energyGen.position;
            energy.gameObject.SetActive(true);
        }, energy =>{
            energy.gameObject.SetActive(false);
        }, energy =>{
            Destroy(energy.gameObject);
        }, true, 10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("e") && canFire){
            if(player.GetComponent<PlayerController>().IsBonus){
                StartCoroutine(fire());
            }
        }
    }

    private IEnumerator fire(){
        canFire = false;
        sound.clip = shootSound;
        sound.Play();
        energyPool.Get();

        yield return new WaitForSeconds(1);
        canFire = true;
    }

    private void ReleaseEnergy(EnergyController energy){
        energyPool.Release(energy);
    }
}
