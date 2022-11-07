using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class EnemyProjectil : MonoBehaviour {

    //Settings
    public float lifeTime;

    //References
    Material material;

    private void  OnTriggerEnter(Collider other){
        if(other.GetComponent<Collider>().tag != "Enemy"){ 
           DestroyWithAnnimation();
        }
    }

    private void Awake(){
        material = GetComponent<Renderer>().material;
    }

    private void DestroyWithAnnimation(){
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.DOScale(transform.localScale.x + 0.25f, 0.25f);
        DOTween.To(()=>material.GetFloat("_Opacity"), (x)=>material.SetFloat("_Opacity", x),0, 0.25f);
        Destroy(gameObject,0.25f);
    }

    void Update (){
        lifeTime = lifeTime - 1 * Time.deltaTime;
        if(lifeTime <= 0)
        {
            FadeAnnimation();
        }
    }

    private void FadeAnnimation(){
        DOTween.To(()=>material.GetFloat("_Opacity"), (x)=>material.SetFloat("_Opacity", x),0, 0.1f);
        Destroy(gameObject,0.1f);
    }



}