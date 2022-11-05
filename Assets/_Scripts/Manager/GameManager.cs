using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake(){
        if(Instance != null){
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
    }

    public int GetCoinsValue(){ 
        return Coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
