using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour{
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

    private Animator myAnim;

    public Transform HealthBar;
    private Image barImage;

    public Transform target;

    public LevelManager theLevelManager;
    public PlayerController player1;

    private Vector3 initialPos = new Vector3(4.84f,-2.87f,0f);

    public bool canMove;
    public bool canJump;
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
        canJump = false;
    }

    // Update is called once per frame
    void Update(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,whatIsGround);
        isWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        isPlayer = Physics2D.OverlapCircle(hitPlayer.position, hitCheckRadius, whatIsPlayer);
        
        if(canMove && Time.timeScale > 0){ 
            if(target.transform.position.x +3f < transform.position.x || target.transform.position.x-3f > transform.position.x){
                if(target.transform.position.x > transform.position.x)
                        myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);//right
                if(target.transform.position.x < transform.position.x)
                    myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);//left
                    
            }else{
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);//stand 
            }
            if(isPlayer){
               int action = Random.Range(1, 200);
               switch(action){            
                    case 3: //kick 
                        myAnim.SetBool("Kicking", true);
                        break;
                    case 2://punch
                        myAnim.SetBool("Punching", true);
                        break;
                    case 1://block
                        myAnim.SetBool("Blocking", true);
                        break;
                    default:   
                        break;
                }
                canJump = false;
            }
           
            if(target.transform.position.x < transform.position.x){
                    //condicao para ficar de frente para o outro player
                    transform.localScale = new Vector3(0.7f,0.7f,0.7f);
                }else{
                    transform.localScale= new Vector3(-0.7f,0.7f,0.7f);
            }
            /*
            if(Input.GetButtonDown("") && isWall){
                myAnim.SetBool("WallJump", isWall);
            }
            */              
           if(myAnim.GetBool("Punching") && isGrounded){
                punchSound.Play();
                if(isPlayer){
                    MakeDamage(0.1f);
                }      
            }

            if(myAnim.GetBool("Kicking")){
                kickSound.Play();
                if(isPlayer){
                     MakeDamage(0.15f);
                }
            }    
        }   

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
        
        myAnim.SetBool("Blocking", false);
        myAnim.SetBool("Punching", false);
        myAnim.SetBool("Kicking", false);
        
        
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
