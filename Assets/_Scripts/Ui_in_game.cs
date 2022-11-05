using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Ui_in_game : MonoBehaviour
{

    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void Awake() {
        // coinsText = GetComponent<TextMeshPro>();    
    }
    // Update is called once per frame
    void Update()
    {
        coinsText.text = GameManager.Instance.GetCoinsValue().ToString();
    }
}
