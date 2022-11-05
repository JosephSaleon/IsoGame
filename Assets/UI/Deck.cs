using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class Deck : MonoBehaviour
{

    public DeckManager deckManager;
    VisualElement root;
    VisualElement deck;
    VisualTreeAsset DeckColTemplate;
    List<List<Card>> deckList;
    private void OnEnable()
    {
        deckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        root = GetComponent<UIDocument>().rootVisualElement;

        deck = root.Q("Deck");
        DeckColTemplate = Resources.Load<VisualTreeAsset>("DeckCol Template");
        deckList = deckManager.GetDeck();

        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 2);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 2);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 2);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 1);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 0);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 1);
        deckManager.AddCardToDeck(UITool.GetRandomCards(1)[0], 1);


        UpdateUI();

        
    }

    private void ResetUI(){
        deck.Clear();
    }

    private void UpdateUI(){ 
        ResetUI();
        DisplayUI();
    }
    private void DisplayUI()
    {
        for(int i = 0; i < deckManager.maxCol; i++)
        {   
            VisualElement newCol = DeckColTemplate.Instantiate();
            newCol.name = i.ToString();
            newCol.RegisterCallback<ClickEvent>(OnClickCol);
            foreach(Card card in deckList[i]){
                VisualElement newCard = UITool.CreateCardUIDeck(card);
                newCol.Add(newCard);
            }
            deck.Add(newCol);
        }
    }

    private void OnClickCol(ClickEvent evt){
        int index = Int32.Parse((evt.currentTarget as VisualElement).name);
        if(deckManager.GetSelectedCard() != null)
            deckManager.AddCardToDeck(deckManager.UseSelectedCard(),index);
            UpdateUI();
        
    }
}
