using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class CardReward : MonoBehaviour
{
    VisualElement root;
    int numberOfPikupCards = 3;

    public UIManager uIManager;
    public DeckManager deckManager;
    private System.Random rnd = new System.Random();

    private Card[] rndCards;

    private void OnEnable()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        deckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();


        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement cardView = root.Q("CardView");
        rndCards = UITool.GetRandomCards(numberOfPikupCards);
        int index = 0;
        foreach (Card card in rndCards)
        {
            VisualElement newCard = UITool.CreateCardUI(card);
            newCard.name = index.ToString();
            newCard.RegisterCallback<ClickEvent>(OnClickCard);
            cardView.Add(newCard);
            index++;
        }     
    }

    private void OnClickCard(ClickEvent evt){
        int index = Int32.Parse((evt.currentTarget as VisualElement).name);
        deckManager.SelectCard(rndCards[index]);
        uIManager.SetScreen("deck");
    }
}
