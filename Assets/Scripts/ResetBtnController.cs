using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBtnController : MonoBehaviour
{
    GameObject restartButton;
    private bool isBtnActive = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject originalGameObject = GameObject.Find("BtnGameObject");
        restartButton = originalGameObject.transform.GetChild(0).gameObject;
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.gameEnded){
             if (!isBtnActive){
                restartButton.SetActive(true);
            }
        }else{
            if (isBtnActive){
                restartButton.SetActive(false);
            }
        }
    }
}
