using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

 public enum GameState {Stanby, Playing, Ended};

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.02f;
    public RawImage background;
    public RawImage ground;
    public AudioSource music;
    public AudioSource DestroySound;

    public GameState gameState = GameState.Stanby;

    public GameObject Shark_Generator;
    public GameObject BubbleGenerator;
    public GameObject ObjGenerator;
    public GameObject CoinGenerator;
    public GameObject UiStanby;
    public GameObject UiGameOver;
    public GameObject ObjBestScore;


    public float scaleTime = 10f;
    public float scaleInc = .1f;
    private int points = 0;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI bestScoreText;


    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        InvokeRepeating("increasePoints", 2f, 1f);
        bestScoreText.text = "BEST SCORE: " + getBestScore().ToString();
        ObjBestScore = GameObject.FindGameObjectWithTag("BestScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Stanby && Input.GetKeyDown(KeyCode.Space))
        {
            gameState = GameState.Playing;
            music.Play();
            Shark_Generator.SendMessage("StartGen");
            BubbleGenerator.SendMessage("StartGen");
            ObjGenerator.SendMessage("StartGen");
            CoinGenerator.SendMessage("StartGen");
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
        }
        else if (gameState==GameState.Playing)
        {
            ObjBestScore.SetActive(false);
            UiStanby.SetActive(false);
            parallax();
        }
        else if (gameState==GameState.Ended)
        {
            music.Stop();
            if( points > getBestScore()){
                setBestScore(points);
                bestScoreText.text = "NEW BEST SCORE: " + getBestScore().ToString();
            }
            ObjBestScore.SetActive(true);
            UiGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)){
                BackMenu();
            }
            
        }

    }

    void parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        ground.uvRect = new Rect(ground.uvRect.x + finalSpeed *3, 0f, 1f, 1f);
    }

    public void BackMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void GameTimeScale(){
        Time.timeScale += scaleInc;
    }

    public void ResetTimeScale(){
        CancelInvoke("GameTimeScale");
        Time.timeScale = 1f;
    }

    public void increasePoints(){
        if(gameState==GameState.Playing){
            points++;
            pointsText.text = points.ToString();
        }
    }

    public void extraPoints(int numPoints){
            points += numPoints;
            pointsText.text = points.ToString();
    }

    public int getBestScore(){
        return PlayerPrefs.GetInt("Best Score", 0);
    }

    public void setBestScore(int score){
        PlayerPrefs.SetInt("Best Score", score);
    }

    public void dieSound(){
        DestroySound.Play();
    }
}
