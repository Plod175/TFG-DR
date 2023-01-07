using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private GameObject game;
    private GameObject BonusSign;
    public GameObject Shark_Generator;
    public GameObject BubbleGenerator;
    public GameObject ObjGenerator;
    public GameObject CoinGenerator;
    public ParticleSystem DestructionEffect;
    private AudioSource audioPlayer;
    public AudioClip dashSound;
    public AudioClip bonusSound;

    private float hMove = 0f;
    private float vMove = 0f;
    private int CoinPoint = 3;
    
    [SerializeField] private float Vel;
    [SerializeField] private float DashVel;
    [SerializeField] private float DashTime;


    public bool right = true;
    private bool canDash = true;
    private bool canMove = true;
    public bool IsBonus = false;
    public bool IsBaseDolp = true;
    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        game = GameObject.FindGameObjectWithTag("MainCanvas");
        BonusSign = GameObject.FindGameObjectWithTag("BonusSign");
        Shark_Generator = GameObject.Find("SharkGenerator");
        BubbleGenerator = GameObject.Find("BubbleGenerator");
        ObjGenerator = GameObject.Find("ObjGenerator");
        CoinGenerator = GameObject.Find("CoinGenerator");
        BonusSign.SetActive(false);

        if(GameManager.Instance){
            int indexpj = PlayerPrefs.GetInt("PlayerIndex");
            if(indexpj == 1){
                IsBaseDolp = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetComponent<GameController>().gameState == GameState.Playing){

            hMove = Input.GetAxisRaw("Horizontal") * Vel;
            vMove = Input.GetAxisRaw("Vertical") * Vel;

            if(Input.GetKeyDown(KeyCode.Space) && canDash) {
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate() {
        if (vMove != 0 && canMove){
            vMovement(vMove* Time.fixedDeltaTime);
        }else if (canMove){
            hMovement(hMove* Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash() {
        canMove = false;
        canDash = false;
        audioPlayer.clip = dashSound;
        audioPlayer.Play();
        rb2d.velocity = new Vector2(DashVel * transform.localScale.x,0);
        animator.SetTrigger("Dash");
        yield return new WaitForSeconds(DashTime);

        canMove = true;
        canDash = true;
    }

    private void hMovement(float move) {
        Vector2 objVel = new Vector2(move, rb2d.velocity.y);
        rb2d.MovePosition(rb2d.position + objVel);

        if(move>0 && !right){
            Girar();
        }else if(move<0 && right){
            Girar();
        }
    }

    private void vMovement(float move) {
        Vector2 objVel2 = new Vector2(0, move);
        rb2d.MovePosition(rb2d.position + objVel2);
    }

    private void Girar (){
        right = !right;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if(c2d.tag == "Enemy"){
            game.SendMessage("dieSound");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            Shark_Generator.SendMessage("CacelGen", true);
            BubbleGenerator.SendMessage("CacelGen");
            ObjGenerator.SendMessage("CacelGen", true);
            CoinGenerator.SendMessage("CacelGen", true);
            game.SendMessage("ResetTimeScale");
            Explode();
        } else if(c2d.tag == "Coin"){
            if(IsBonus && IsBaseDolp){
                game.SendMessage("extraPoints", CoinPoint * 2);
            }else{
                game.SendMessage("extraPoints", CoinPoint);
            }
        } else if(c2d.tag == "Bonus"){
            audioPlayer.clip = bonusSound;
            audioPlayer.Play();
            StartCoroutine(Bonus());
        }
    }

    void Explode()
     {
        ParticleSystem explosionEffect = Instantiate(DestructionEffect) as ParticleSystem;
        var otherPosn = transform.position;
        explosionEffect.transform.position = new Vector3(otherPosn.x, otherPosn.y, -1f);
        explosionEffect.Play();
        Destroy(explosionEffect.gameObject, 5f);
        Destroy(gameObject);
     }

     private IEnumerator Bonus(){
        IsBonus = true;
        BonusSign.SetActive(true);
        yield return new WaitForSeconds(10);
        BonusSign.SetActive(false);
        IsBonus = false;
     }


}
