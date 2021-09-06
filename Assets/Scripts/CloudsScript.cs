using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour{

    private Vector3 initialPos = new Vector3(-20f,4.3f,0f);

    private static float moveSpeed = 0.1f;
    private Rigidbody2D myRigidbody;
    
      
    // Start is called before the first frame update
    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        
        if(transform.position.x >20f){
            transform.localScale = initialPos;
        }
       
        myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
    }
}
