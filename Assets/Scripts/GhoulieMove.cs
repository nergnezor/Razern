﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GhoulieMove : MonoBehaviour
{
    
    public float healthAmount;
    public Text healthPointsText;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpPowerBoost = 4f;
    bool move;
    Vector2 newPos;
    bool gameEnded = false;

    //Jump Related
    public float jumpPower;
    public bool isGrounded = false;
    public bool jumpPtrDown = false;
    public LayerMask groundLayers;

    // Particle System Collision (Flying Ghosts)
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    
    // Start is called before the first frame update
    void Start()
    {

        healthPointsText.text = "HP " + healthAmount.ToString();

        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
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
            if (gameEnded) return;
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

    }
    private void MoveCharFromUiButtons(){
        if (gameEnded) return;
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
public void touchButtonJumpPtrUp(){
    jumpPtrDown = false;
}
public void touchButtonJumpPtrDw(){
    if (jumpPtrDown) return; // Avoid pointer down fireing a jump call every frame
    jumpPtrDown = true;
    Jump();

}
    public void Jump(){

        isGrounded = Physics2D.OverlapArea (new Vector2 (transform.position.x - 2f, transform.position.y - 0.001f),
        new Vector2 (transform.position.x + 2f, transform.position.y - 0.001f), groundLayers);
        if (isGrounded == true){
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "EnemyAxe") {
            TakeDamage(other.gameObject, 25f);
            
        }else if (other.gameObject.tag == "EnemyCell") {
            TakeDamage(other.gameObject, 10f);

        }else if (other.gameObject.tag == "Mom"){
            // Add you found Mom
            gameEnded = true;
        }

      //  Debug.Log("Tag is: ");
      //  Debug.Log(other.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other) {

       if (other.gameObject.tag == "MagicBubbles"){
            jumpPower += jumpPowerBoost;
        Debug.Log("TRIGGER ");
        }

        
      //  Debug.Log(other.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D other) {

       if (other.gameObject.tag == "MagicBubbles"){
            jumpPower -= jumpPowerBoost;
        Debug.Log("TRIGGER ");
        }

        
      //  Debug.Log(other.gameObject.tag);
    }

     private void OnCollisionExit2D(Collision2D other) {

     }

    private void TakeDamage(GameObject otherGameObject, float damage){
            healthAmount -= damage;
            healthPointsText.text = "HP " + healthAmount.ToString();
            Vector3 gameObjectPos = gameObject.transform.position;
            float enemyPosX = otherGameObject.transform.position.x;
            gameObject.transform.position = (gameObjectPos.x < enemyPosX) ? gameObjectPos + new Vector3(-2, 0, 0) : gameObjectPos + new Vector3(2, 0, 0);

            if (healthAmount <= 0){
                Destroy(gameObject);
                Loader.Load(Loader.Scene.MainScene);
            }
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("HIIIIIIITT");
        TakeDamage(other, healthAmount);
      /*  int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }
            i++;
        }*/
    }
    
}
