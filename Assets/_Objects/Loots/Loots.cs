using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loots : MonoBehaviour
{

    public Rigidbody rb;
    private GameManager gameManager;
    public System.Random rnd = new System.Random();
    public int value = 1; 

    private void Start() {
        gameManager = GameManager.Instance;
    }

    public void pickUpLoot(){
        gameManager.addCoins(value);
        Destroy(gameObject);
    }

  

}


