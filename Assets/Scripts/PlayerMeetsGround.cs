using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeetsGround : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        print("Script runs");
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onCollisionEnter(Collision collision){

        //if (collision.collider.tag == "Ground"){
            print("GROUND");
            Player.GetComponent<GhoulieMove>().isGrounded = true;
        //}

    }

    private void onCollisionExit2D(Collision2D collision){
        //if (collision.collider.tag == "Ground"){
            print("NOT GROUND");
            Player.GetComponent<GhoulieMove>().isGrounded = false;
            
        // }
    }

    //Just overlapped a collider 2D
private void OnTriggerEnter2D(Collider2D collision)
{
//Do something
print("TRIGGER");
}

}

