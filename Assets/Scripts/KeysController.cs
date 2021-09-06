using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour{


    public GameObject keysScreen;

    public Dictionary<string, KeyCode> keysP1 = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> keysP2 = new Dictionary<string, KeyCode>();

    public Text leftP1, rightP1, jumpP1, punchP1, kickP1, blockP1, wallP1;
    public Text leftP2, rightP2, jumpP2, punchP2, kickP2, blockP2, wallP2;

    private GameObject currentKeyP1;
    private GameObject currentKeyP2;

    public MainMenu menu;
    
    // Start is called before the first frame update
    void Start(){
        menu = FindObjectOfType<MainMenu>();

        keysP1= menu.keysP1;
        keysP2 = menu.keysP2;

        leftP1.text = keysP1["left"].ToString();
        rightP1.text = keysP1["right"].ToString();
        jumpP1.text = keysP1["jump"].ToString();
        punchP1.text = keysP1["punch"].ToString();
        kickP1.text = keysP1["kick"].ToString();
        blockP1.text = keysP1["block"].ToString();
        wallP1.text = keysP1["wall"].ToString();

        leftP2.text = keysP2["left"].ToString();
        rightP2.text = keysP2["right"].ToString();
        jumpP2.text = keysP2["jump"].ToString();
        punchP2.text = keysP2["punch"].ToString();
        kickP2.text = keysP2["kick"].ToString();
        blockP2.text = keysP2["block"].ToString();
        wallP2.text = keysP2["wall"].ToString();
    }

    // Update is called once per frame
    void Update(){
         if(Input.GetKeyDown(KeyCode.Escape)){
             backToMenu();
         }
    }

    void OnGUI(){
        if(currentKeyP1 != null){
            Event e = Event.current;
            if(e.isKey){
                keysP1[currentKeyP1.name] = e.keyCode;
                currentKeyP1.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKeyP1 = null;
            }
        }
        if(currentKeyP2 != null){
            Event e = Event.current;
            if(e.isKey){
                keysP2[currentKeyP2.name] = e.keyCode;
                currentKeyP2.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKeyP2 = null;
            }
        }
    }
    public void ChangeKeyP1(GameObject cliked){
        currentKeyP1 = cliked;
    }
    public void ChangeKeyP2(GameObject cliked){
        currentKeyP2 = cliked;
    }
    public void backToMenu(){
        keysScreen.SetActive(false);  
        getKeys(); 
    }

    public void getKeys(){
        menu.keysP1 = keysP1;
        menu.keysP2 = keysP2;
    }
    void saveKeys(){

    }
}
