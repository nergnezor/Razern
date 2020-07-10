using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     Debug.Log("Starting");   
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("AAAAAJ AAAAAAJ");
    }
}
