using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulieMove : MonoBehaviour
{
    
    public static float healthAmount;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpPower;

    //isGrounded
    public bool isGrounded = false;
    public LayerMask groundLayers;
    // Start is called before the first frame update
    void Start()
    {

        healthAmount = 1;

       //   rb = GetComponent<Rigidbody2D>();

        // Moves the GameObject using it's transform.
        //rb.isKinematic = true;
    //    gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
     //   gameObject.GetComponent<Rigidbody2D>().detectCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0){
            Destroy (gameObject);
        }
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        //rb.MovePosition(transform.position + movement * Time.deltaTime * moveSpeed);
        
    }

    void Jump(){
        isGrounded = Physics2D.OverlapArea (new Vector2 (transform.position.x - 0.01f, transform.position.y - 0.01f),
        new Vector2 (transform.position.x + 0.01f, transform.position.y - 0.01f), groundLayers);
        if (Input.GetButtonDown("Jump") && isGrounded == true){
        print("JUMP");
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    }

    
        private void onCollisionEnter(Collision collision){

        //if (collision.collider.tag == "Ground"){
            print("GROUND");
            gameObject.GetComponent<GhoulieMove>().isGrounded = true;
        //}

    }
    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "EnemyAxe"){
            healthAmount -= 0.1f;
            gameObject.transform.position = new Vector3(0, -0.5f, 0);

            Debug.Log("OUCH");
        }

        Debug.Log("Tag is: ");
        Debug.Log(other.gameObject.tag);
    }

    
}
