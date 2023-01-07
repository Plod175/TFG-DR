using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    private int index;

    [SerializeField] private Image image;

    [SerializeField] private TextMeshProUGUI pjname;

    private GameManager gameManager;
    private GameObject playerData;
    public GameObject UiAvailable;
    private AudioSource audioMenu;
    private bool hasDolku = true;
    private bool canStart = true;

    // Start is called before the first frame update
    void Start()
    {
        audioMenu = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
        index = PlayerPrefs.GetInt("PlayerIndex");
        playerData = GameObject.FindGameObjectWithTag("PlayerData");
        if(playerData.GetComponent<PlayerDataController>().isData == true){
            hasDolku = playerData.GetComponent<PlayerDataController>().playerHasDolku;
        }

        if (index > gameManager.characters.Count - 1){
            index = 0;
        }

        UpdateChar();


    }

    private void UpdateChar()
    {
        canStart = true;
        UiAvailable.SetActive(false);
        PlayerPrefs.SetInt("PlayerIndex", index);
        image.sprite = gameManager.characters[index].image;
        image.SetNativeSize();
        pjname.text = gameManager.characters[index].charName;
        if(pjname.text == "Dolku" && !hasDolku){
            UiAvailable.SetActive(true);
            canStart = false;
        }
    }

    public void NextChar(){
        audioMenu.Play();
        if (index == gameManager.characters.Count - 1)
        {
            index = 0;
        }else{
            index += 1;
        }
        UpdateChar();
    }

    public void PrevChar(){
        audioMenu.Play();
        if (index == 0)
        {
            index = gameManager.characters.Count - 1;
        }else{
            index -= 1;
        }
        UpdateChar();
    }

    public void PlayButton() {
        audioMenu.Play();
        if(canStart){
            SceneManager.LoadScene(2);
        }
    }

    public void BackButton() {
        StartCoroutine(sound());
    }

    IEnumerator sound(){
         audioMenu.Play();
         yield return new WaitWhile(()=> audioMenu.isPlaying);
         if(canStart){
            SceneManager.LoadScene(0);
        }
     }


}
