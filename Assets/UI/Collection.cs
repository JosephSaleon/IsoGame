using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Collection : MonoBehaviour
{

    VisualElement root;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement cardView = root.Q("CardView");
        Card[] allCards = Resources.LoadAll<Card>("Cards");
        foreach(Card card in allCards)
        {
            VisualElement newCard = UITool.CreateCardUI(card);
            cardView.Add(newCard);
        }     
    }
}
