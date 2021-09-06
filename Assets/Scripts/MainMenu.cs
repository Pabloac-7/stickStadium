using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public string versus;
    //public string vsIA;
    public GameObject keysScreen;

    public AudioSource menuSound;
    public KeysController keysControl;

    public Dictionary<string, KeyCode> keysP1 = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> keysP2 = new Dictionary<string, KeyCode>();

    // Start is called before the first frame update
    void Start(){       
        keysControl = FindObjectOfType<KeysController>();
        
        keysP1.Add("left", KeyCode.A);
        keysP1.Add("right", KeyCode.D);
        keysP1.Add("jump", KeyCode.W);
        keysP1.Add("punch", KeyCode.K);
        keysP1.Add("kick", KeyCode.L);
        keysP1.Add("block", KeyCode.J);
        keysP1.Add("wall", KeyCode.F);

        keysP2.Add("left", KeyCode.LeftArrow);
        keysP2.Add("right", KeyCode.RightArrow);
        keysP2.Add("jump", KeyCode.UpArrow);
        keysP2.Add("punch", KeyCode.Keypad3);
        keysP2.Add("kick", KeyCode.Keypad2);
        keysP2.Add("block", KeyCode.Keypad1);
        keysP2.Add("wall", KeyCode.Keypad0);

        menuSound.Play();
    }

    // Update is called once per frame
    void Update(){     
        

    }

    public void versus1(){

        PlayerPrefs.SetString("leftP2", keysP2["left"].ToString());
        PlayerPrefs.SetString("rightP2", keysP2["right"].ToString());
        PlayerPrefs.SetString("jumpP2", keysP2["jump"].ToString());
        PlayerPrefs.SetString("punchP2", keysP2["punch"].ToString());
        PlayerPrefs.SetString("kickP2", keysP2["kick"].ToString());
        PlayerPrefs.SetString("blockP2", keysP2["block"].ToString());
        PlayerPrefs.SetString("wallP2", keysP2["wall"].ToString());

        PlayerPrefs.SetString("leftP1", keysP1["left"].ToString());
        PlayerPrefs.SetString("rightP1",keysP1["right"].ToString());
        PlayerPrefs.SetString("jumpP1", keysP1["jump"].ToString());
        PlayerPrefs.SetString("punchP1", keysP1["punch"].ToString());
        PlayerPrefs.SetString("kickP1", keysP1["kick"].ToString());
        PlayerPrefs.SetString("blockP1", keysP1["block"].ToString());
        PlayerPrefs.SetString("wallP1", keysP2["wall"].ToString());
        
        //PlayerPrefs.SetInt("vsIA", 1);

        SceneManager.LoadScene(versus);
        menuSound.Stop();
    }
    public void controles(){
       keysScreen.SetActive(true);
    }
    public void quitGame(){
        Application.Quit();
    }

}
