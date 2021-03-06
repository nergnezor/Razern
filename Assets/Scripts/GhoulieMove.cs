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
    public float jumpPowerBoost = 8f;
    bool move;
    Vector2 newPos;
    //bool gameEnded = false;
    private Vector2 physicsPrev;

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

        physicsPrev = Physics2D.gravity; // Used when 
        healthPointsText.text = "HP " + healthAmount.ToString();

        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }

        CompressCharacter();
        MoveCharacter();
            
        

        if (Input.GetButtonDown("Jump"))
            Jump();                         // This is called directly from BtnJump 
       

        void CompressCharacter()
        {
            var scale = transform.localScale;
            if (rb.velocity.y != 0)
                scale.y = 1 - rb.velocity.y / 50;
            else if (scale.y > 1)
                scale.y = Mathf.Max(1, scale.y * 0.8f);
            transform.localScale = scale;
        }
    }
    void MoveCharacter()
    {
        MoveCharacterKeyboardInput();   //
        MoveCharFromUiButtons();        // Input from touch screen

    }

    private void MoveCharacterKeyboardInput()
    {
        if (Globals.gameEnded) return;
        
        //var movement = new Vector2(Input.GetAxis("Horizontal"), 0f);
        
        //rb.AddForce(movement * moveSpeed, ForceMode2D.Force);
        float mH = Input.GetAxis ("Horizontal");
        float mV = Input.GetAxis ("Vertical");
        rb.velocity = new Vector3 (mH * moveSpeed, rb.velocity.y, mV * moveSpeed);

    }
    private void MoveCharFromUiButtons()
    {
        if (Globals.gameEnded) return;
        if (move)
        {
            float mH = Input.GetAxis ("Horizontal");
        //float mV = Input.GetAxis ("Vertical");
        rb.velocity = new Vector3 (newPos.x * moveSpeed, rb.velocity.y, 0f);
        }
    }

    public void leftTouchButton(bool isDown) // called from BtnWalkLeft
    {

        if (isDown)
        {
            move = true;
            newPos.x = -1f;
        }
        else
        {
            move = false;
        }

    }
    public void rightTouchButton(bool isDown) // Called from BtnWalkRight
    {

        if (isDown)
        {
            move = true;
            newPos.x = 1f;
        }
        else
        {
            move = false;
        }
    }
    public void touchButtonJumpPtrUp()
    {
        jumpPtrDown = false;
    }
    public void touchButtonJumpPtrDw()
    {
        if (jumpPtrDown) return; // Avoid pointer down fireing a jump call every frame
        jumpPtrDown = true;
        Jump();
    }
    public void Jump()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 2f, transform.position.y - 0.001f),
        new Vector2(transform.position.x + 2f, transform.position.y - 0.001f), groundLayers);
        if (isGrounded)
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "EnemyAxe":
                TakeDamage(other.gameObject, 25f);
                break;
            case "EnemyCell":
                TakeDamage(other.gameObject, 10f);
                break;
            case "Mom":
                // Add you found Mom
                Globals.foundMom = true;
                Globals.gameEnded = true;
                break;
        }
        // Debug.Log("Tag is: " + other.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "MagicBubbles")
        {
            //jumpPower += jumpPowerBoost;
            // isGrounded = true;
            Physics2D.gravity = new Vector2(0.0f, 2f);
        }


        //  Debug.Log(other.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "MagicBubbles")
        {
            //jumpPower -= jumpPowerBoost;
            // isGrounded = true;
            Physics2D.gravity = physicsPrev;
        }


        //  Debug.Log(other.gameObject.tag);
    }

    private void OnCollisionExit2D(Collision2D other)
    {

    }

    private void TakeDamage(GameObject otherGameObject, float damage)
    {
        healthAmount -= damage;
        healthPointsText.text = "HP " + healthAmount.ToString();
        Vector3 gameObjectPos = gameObject.transform.position;
        float enemyPosX = otherGameObject.transform.position.x;
        gameObject.transform.position = (gameObjectPos.x < enemyPosX) ? gameObjectPos + new Vector3(-1, 0, 0) : gameObjectPos + new Vector3(1, 0, 0);
        //rb.AddForce(-transform.forward * 500);
        //rb.velocity = new Vector3 (newPos.x * -100, rb.velocity.y, 0f);
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
            Globals.gameEnded = true;
            //Loader.Load(Loader.Scene.MainScene);
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
