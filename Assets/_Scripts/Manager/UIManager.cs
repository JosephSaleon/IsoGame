using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject deck;
    public GameObject collection;
    public GameObject cardReward;

    private void Awake() {
       ResetScreen();
       SetScreen("cardReward");
    }

    private void ResetScreen(){ 
        deck.SetActive(true);
        deck.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        collection.SetActive(true);
        collection.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
        cardReward.SetActive(true);
        cardReward.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    public void SetScreen(string UIName){
        ResetScreen();
        if(UIName == "deck"){
            deck.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
        if(UIName == "collection"){
            collection.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
        if(UIName == "cardReward"){
            cardReward.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

}
