using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene1(){
        Globals.gameEnded = false;
        Loader.Load(Loader.Scene.MainScene);
    }
}
