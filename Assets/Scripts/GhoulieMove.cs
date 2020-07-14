using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GhoulieMove : MonoBehaviour
{
    
    public static float healthAmount;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpPower;
    bool move;
    Vector2 newPos;

    //isGrounded
    public bool isGrounded = false;
    public LayerMask groundLayers;
    // Start is called before the first frame update
    void Start()
    {

        healthAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0){
            Destroy (gameObject);
        }
        
        MoveCharacter();
        
        

        
        
    }

    void MoveCharacter(){

                    
            MoveCharacterKeyboardInput();   //
            MoveCharFromUiButtons();        // Input from touch screen

        if (Input.GetButtonDown("Jump")){

            Jump();                         // This is called directly from BtnJump 
            
        }
    }

    private void MoveCharacterKeyboardInput(){

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

    }
    private void MoveCharFromUiButtons(){
    
        if(move){
            Vector3 movement = new Vector3(newPos.x, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;
        }
    }
    
    public void leftButton(bool isDown){

        if(isDown){
            move = true;
            newPos.x = -1f;
        }else{
            move = false;
        }
    
    }
    public void rightButton(bool isDown){
    
        if(isDown){
            move = true;
            newPos.x = 1f;
        }else{
            move = false;
        }
}

    public void Jump(){

        isGrounded = Physics2D.OverlapArea (new Vector2 (transform.position.x/* - 0.001f*/, transform.position.y - 0.001f),
        new Vector2 (transform.position.x/* + 0.001f*/, transform.position.y - 0.001f), groundLayers);
        if (isGrounded == true){
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "EnemyAxe" || other.gameObject.tag == "EnemyCell"){
            healthAmount -= 0.1f;
            Vector3 gameObjectPos = gameObject.transform.position;
            float enemyPosX = other.gameObject.transform.position.x;
            gameObject.transform.position = (gameObjectPos.x < enemyPosX) ? gameObjectPos + new Vector3(-2, 0, 0) : gameObjectPos + new Vector3(2, 0, 0);

            if (healthAmount <= 0){
                Destroy(gameObject);
            }
            Debug.Log("OUCH");
        }else if (other.gameObject.tag == "Mom"){
            // Add you found Mom
        }

        Debug.Log("Tag is: ");
        Debug.Log(other.gameObject.tag);
    }
    
}
