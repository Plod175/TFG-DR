using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController Instance;
    public bool playerHasDolku;
    public string playerName;
    public bool isData = false;

    void Awake(){
        if (PlayerDataController.Instance == null)
        {
            PlayerDataController.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerHasDolku(int numDolps){
        if(numDolps == 2){
            playerHasDolku = true;
        }else{
            playerHasDolku = false;
        }
        isData = true;
    }

    public void SetPlayerName(string wallet){
        playerName = wallet;
    }


}
