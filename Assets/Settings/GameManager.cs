using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake(){
        if(Instance != null){
            Debug.Log("GameManager Exists!");
            return;
        }
        Instance = this;
    }
    
    public int Coins = 0;
    

    void Start()
    {
        
    }

    public void addCoins(int value){
        Coins += value;
        Debug.Log("yesssss" + Coins);
    }

    public int GetCoinsValue(){ 
        return Coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
