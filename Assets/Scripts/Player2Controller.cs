using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour{

    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D myRigidbody;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    
    public Transform hitPlayer;
    public float hitCheckRadius;
    public LayerMask whatIsPlayer;

    public bool isGrounded;
    public bool isWall;
    public bool isPlayer;
    public bool jumping;

    private float timeJump;
    private float jumped;
    private float doubleRightTime;
    private float doubleLeftTime;
    private float position;
    private float hitTime;
        
    private Animator myAnim;

    public Transform HealthBar;
    private Image barImage;

    public Transform target;

    public LevelManager theLevelManager;
    public PlayerController player1;

    private Vector3 initialPos = new Vector3(4.84f,-2.87f,0f);

    public bool canMove;
    private bool doubleRight;
    private bool doubleLeft;
    private bool dashOn;
    
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode punch;
    public KeyCode kick;
    public KeyCode block;
    public KeyCode wallJump;

    public AudioSource jumpSound;
    public AudioSource kickSound;
    public AudioSource punchSound;

    // Start is called before the first frame update
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        barImage = HealthBar.transform.Find("bar").GetComponent<Image>();
        
        theLevelManager = FindObjectOfType<LevelManager>();
        player1 = FindObjectOfType<PlayerController>();

        canMove = false;

        if(PlayerPrefs.HasKey("leftP2"))
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftP2"));
        if(PlayerPrefs.HasKey("rightP2"))
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightP2"));
        if(PlayerPrefs.HasKey("jumpP2"))
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpP2"));
        if(PlayerPrefs.HasKey("punchP2"))
            punch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("punchP2"));
        if(PlayerPrefs.HasKey("kickP2"))
            kick = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("kickP2"));
        if(PlayerPrefs.HasKey("blockP2"))
            block = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("blockP2"));
        if(PlayerPrefs.HasKey("wallJump"))
            wallJump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("wallJump"));
    }

    // Update is called once per frame
    void Update(){

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
        isWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        isPlayer = Physics2D.OverlapCircle(hitPlayer.position, hitCheckRadius, whatIsPlayer);
        if(canMove && Time.timeScale > 0){ 
            //flip control
            if(target.transform.position.x < transform.position.x)
                transform.localScale = new Vector3(0.7f,0.7f,0.7f);
            else
                transform.localScale= new Vector3(-0.7f,0.7f,0.7f);
            //walk controll
            if(Input.GetKey(right))//right
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            else if(Input.GetKey(left))//left
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);           
            else
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            
            //Dash controll
            if (Input.GetKeyDown(right)){
                if ((Time.time < doubleRightTime + .3f) && !isWall){
                    doubleRight = true;
                    dashOn =true;
                    position = transform.position.x +2f;
                    if(position > 9.5f)
                        position = 9f;
                }
                doubleRightTime = Time.time;
            }
            if(doubleRight){
                if(position > transform.position.x && !(position+1.2 < target.position.x))
                    myRigidbody.velocity = new Vector3((moveSpeed*4), myRigidbody.velocity.y, 0f);
                else{
                    doubleRight =false;
                    dashOn =false;
                }
            }
            if (Input.GetKeyDown(left)){
                if (Time.time < doubleLeftTime + .3f){
                    doubleLeft = true;
                    dashOn =true;
                    position = transform.position.x-2f;
                    if(position < -9.5f)
                        position = -9f;
                }
                doubleLeftTime = Time.time;
            }
            if(doubleLeft){
                if (position < transform.position.x && !(position-1.2 < target.position.x))
                    myRigidbody.velocity = new Vector3((-moveSpeed*4), myRigidbody.velocity.y, 0f);
                else{
                    doubleLeft =false;
                    dashOn =false;
                }
            }
            //jump controll
            if(Input.GetKeyDown(jump) && isGrounded){
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
            //walljump controll
            timeJump = theLevelManager.tempo; 
            if(Input.GetKeyDown(wallJump) && isWall && !jumping){
                myAnim.SetBool("closeWall", isWall);
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed+2, 0f);
                jumpSound.Play();
                jumping =true;   
                jumped = timeJump;
            }      
            if(jumping && (timeJump+0.5 == jumped || timeJump+0.5 < jumped )){
                if(transform.position.x < 0){
                    myRigidbody.velocity = new Vector3((moveSpeed*2), myRigidbody.velocity.y, 0f);
                    if(transform.position.x > -3.8f){
                        jumping =false;
                        myAnim.SetBool("closeWall", jumping);
                    }
                }else{
                    myRigidbody.velocity = new Vector3((-moveSpeed*2), myRigidbody.velocity.y, 0f);
                    if(transform.position.x < 4.1f){
                        jumping =false;
                        myAnim.SetBool("closeWall", jumping);
                    }
                }   
            }
            myAnim.SetBool("Dashing", dashOn);
            myAnim.SetBool("Blocking", Input.GetKey(block));
            
            if(Input.GetKeyDown(punch)){
                if(Time.time < hitTime + 1.2f)
                    myAnim.SetBool("Punching", true);
                hitTime = Time.time;
            }else
                myAnim.SetBool("Punching", false);
            
            if(Input.GetKeyDown(kick)){
                if(Time.time < hitTime + 1.2f)
                     myAnim.SetBool("Kicking", true);
                hitTime = Time.time;
            }else
                myAnim.SetBool("Kicking", false);

            if(myAnim.GetBool("Punching") && isGrounded){
                punchSound.Play();
                if(isPlayer)
                    MakeDamage(0.08f);     
            }

            if(myAnim.GetBool("Kicking")){
                    kickSound.Play();
                    if(isPlayer)
                        MakeDamage(0.1f); 
            } 
        }   
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
    }

    public void TakeDamage(float damage){
        if(myAnim.GetBool("Blocking")==false){
            barImage.fillAmount -= damage;
        } 
        if(barImage.fillAmount <= 0f){
            theLevelManager.Player1Won();
        }
    }
    public void MakeDamage(float damage){     
        player1.TakeDamage(damage);
    }
   
    public float getLife(){
        return barImage.fillAmount;
    }

    public void reset(){
        transform.localScale = initialPos;
        canMove = false;
        barImage.fillAmount = 1f;
    }
}
