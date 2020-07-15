using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YouFoundMom : MonoBehaviour
{
    public Text youFoundMomText;

    private void Start() {
        youFoundMomText.gameObject.SetActive(false);
    }
        private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Player"){
            Debug.Log("YOU WIN");
            youFoundMomText.gameObject.SetActive(true);
        }

        Debug.Log("Tag is: ");
        Debug.Log(other.gameObject.tag);
    }

}
