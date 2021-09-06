using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour{
   
    public PlayerController player1;
    public Player2Controller player2;
    
    public GameObject P1;
    public GameObject P2;
    public GameObject winnerP1;
    public GameObject winnerP2;
    public GameObject draw;
    public GameObject fight;
    public GameObject counter;
    public GameObject pauseScreen;
    public GameObject blackScreen;

    private Text counterTime;

    private float timerFight;
   
    public float tempo;
    
    public Image barImage3;
    public Image barImage1;
    public Image barImage2;
    public Image barImageLutem;
    public Image barImageKO;

    private static float initialCount = 4f;
    private static float initialTime = 90f;


    public string menu;

    public AudioSource fightSound;

    private bool canCount;

    // Start is called before the first frame update
    void Start() {
        blackScreen.SetActive(false);

        player1 = FindObjectOfType<PlayerController>();
        player2 = FindObjectOfType<Player2Controller>();   

        counterTime = counter.GetComponent<Text>();
        
        timerFight = initialCount;
        tempo = initialTime;
        canCount = false;
       
        fightSound.Play();
    }

    // Update is called once per frame
    void Update(){
        timerFight -= Time.deltaTime;

        if(timerFight < 0){
            fight.SetActive(false);
            barImageLutem.enabled = false;
            canCount = true;
            MovePlayers(true);
        }else if(timerFight > 0 && timerFight < 1){
            barImage1.enabled = false;
            barImageLutem.enabled = true;
        }else if(timerFight > 1 && timerFight < 2){
            barImage2.enabled = false;
            barImage1.enabled = true;
        }
        else if(timerFight > 2 && timerFight < 3){
            barImage3.enabled = false;
            barImage2.enabled = true;
        }else{
            barImage3.enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        if(canCount){
            tempo -= Time.deltaTime;
            if(tempo < 0){ 
                tempo = 0;
                TimeOver();
            }
            counterTime.text = "- " + Mathf.Round(tempo) + " -";
        } 
        
    }

    public void Player1Won(){
        barImageKO.enabled = true;
        StartCoroutine(waitKO(true));
    }
    public void Player2Won(){
        barImageKO.enabled = true;
        StartCoroutine(waitKO(false));
    }
    void Drew(){
        draw.SetActive(true);
        P1.SetActive(false);
        P2.SetActive(false);
        blackScreen.SetActive(true);
        goToMenu();
    }

    public void goToMenu(){
        StartCoroutine("wait");
    }
    IEnumerator waitKO(bool winner){
        P1.SetActive(false);
        P2.SetActive(false);
        yield return new WaitForSeconds(3);

        barImageKO.enabled = false;
        if(winner)
            winnerP1.SetActive(true);
        else
            winnerP2.SetActive(true);
        
        blackScreen.SetActive(true);    
        goToMenu();    
    }
    IEnumerator wait() {
       
        yield return new WaitForSeconds(3);
        fightSound.Stop();
        SceneManager.LoadScene(menu);
       
    } 
    
    public void goToMainMenu(){
        Time.timeScale = 1f;
        blackScreen.SetActive(false);
        SceneManager.LoadScene(menu);
        fightSound.Stop();
        
    }

    void MovePlayers(bool move){
        player1.canMove = move;
        player2.canMove = move;      
    }
    
    void TimeOver(){    
        if(player1.getLife() > player2.getLife()){
                Player1Won();
            }else if(player1.getLife() < player2.getLife()){
                Player2Won();
        }else{
                Drew();
        }
    }
    public void ResumeGame(){
        pauseScreen.SetActive(false);       
        Time.timeScale = 1f;
    }
}