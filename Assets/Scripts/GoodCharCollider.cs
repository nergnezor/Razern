using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCharCollider : MonoBehaviour
{    void Start()
    {
        Debug.Log("Starting collider script");
    //    gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
     //   gameObject.GetComponent<Rigidbody2D>().detectCollisions = true;
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log(("HIT Collision"));
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log(("HIT Trigger"));
    }}
