using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyProjectil : MonoBehaviour {


    
    

    // private void OnCollisionEnter(Collision other) {
    //     if(other.collider.tag == "Enemy"){ 
    //         Debug.Log("Enemy");
    //     }
    // }

    private void  OnTriggerEnter(Collider other){
        if(other.GetComponent<Collider>().tag != "Enemy"){ 
            Destroy(gameObject);
        }
    }



}