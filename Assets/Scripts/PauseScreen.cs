using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour{

    public LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start() {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 0f){
                theLevelManager.ResumeGame();        
            }        
        }
    }

    public void ResumeGame(){
        theLevelManager.ResumeGame();
    }
    public void QuitToMainMenu(){
        theLevelManager.goToMainMenu();
    }

}
