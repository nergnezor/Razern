using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTextMesh : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer textMeshRend;

    void Start()
    {
        textMeshRend = GetComponent<MeshRenderer> ();
        Color c = textMeshRend.material.color;
        c.a = 0.0f;
        textMeshRend.material.color = c;
    }

    // Update is called once per frame
    void Update()
    {

    }
        IEnumerator FadeIn(){

            
        for (float f = f = textMeshRend.material.color.a; f < 1f; f += 0.05f)
        {
            Color c = textMeshRend.material.color;
            c.a = f;
            textMeshRend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }        
    }

    public void StartFadeIn(){
        StopCoroutine("FadeOut");
        StartCoroutine("FadeIn");
    }

     IEnumerator FadeOut(){
      
        for (float f = textMeshRend.material.color.a; f >= -0.05f; f -= 0.05f){
            Color c = textMeshRend.material.color;
            c.a = f;
            textMeshRend.material.color = c;
            yield return new WaitForSeconds(0.05f);
         }
        }
    public void StartFadeOut(){
            StopCoroutine("FadeIn");
            StartCoroutine("FadeOut");
        }
   
}
