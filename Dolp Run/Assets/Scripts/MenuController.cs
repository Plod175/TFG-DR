using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private AudioSource audioMenu;

    public void Play(){
        StartCoroutine(sound(2));
    }

    public void CharSelection(){
        StartCoroutine(sound(1));
    }

    public void Exit(){
        audioMenu.Play();
        Application.Quit();
    }

    IEnumerator sound(int index){
         audioMenu.Play();
         yield return new WaitWhile(()=> audioMenu.isPlaying);
         SceneManager.LoadScene(index);
     }


    // Start is called before the first frame update
    void Start()
    {
        audioMenu = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
