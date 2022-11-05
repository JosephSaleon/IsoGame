using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class DeckManager : MonoBehaviour
{

    public List<List<Card>> deck = new List<List<Card>>();
    public int maxCol = 4;
    private bool isInit = false;
    private Card selectedCard;

    private void initDeck(){  

        for(int i = 0; i < maxCol ;i++){
            deck.Add(new List<Card>());
        }
        isInit = true;
    }

    public void SelectCard(Card card){ 
        selectedCard = card;
    }

    public Card GetSelectedCard(){ 
        return selectedCard;
    }

    public Card UseSelectedCard(){ 
        Card copySelectedCard = selectedCard;
        selectedCard = null;
        return copySelectedCard;
    }
   
    public void AddCardToDeck(Card card, int colIndex){
        if(!isInit){
            initDeck();
        }
        if(colIndex > maxCol - 1){ 
            return;
        }
        
        deck[colIndex].Add(card);
    }
    public void AddColToDeck(){
        maxCol += 1;
        deck.Add(new List<Card>());
    }

    public List<List<Card>> GetDeck(){ 
        if(!isInit){
            initDeck();
        }
        return deck;
    }
}
