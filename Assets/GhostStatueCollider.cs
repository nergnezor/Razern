using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostStatueCollider : MonoBehaviour
{

    private GameObject ghostTextMesh;
    // Start is called before the first frame update
    void Start()
    {
        ghostTextMesh = GameObject.Find("GhostText"); // Grab the object
        ghostTextMesh.SetActive(false);               // It should not be visable at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ghostTextMesh.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Ghost");
        ghostTextMesh.SetActive(false);

    }

    
   /* private void OnCollisionEnter2D(Collision2D other)
    {
       // Debug.Log("Enter Trigger Ghost");
       // ghostTextMesh.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Exit Trigger Ghost");
        ghostTextMesh.SetActive(false);
       // ghostTextMesh. = false;


        //  Debug.Log(other.gameObject.tag);
    }*/
}
