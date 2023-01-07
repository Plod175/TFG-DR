using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPos : MonoBehaviour
{
    public GameObject baseDolp;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance){
            int indexpj = PlayerPrefs.GetInt("PlayerIndex");
            Instantiate(GameManager.Instance.characters[indexpj].characterSel, transform.position, Quaternion.identity);
        }else{
            Instantiate(baseDolp, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
