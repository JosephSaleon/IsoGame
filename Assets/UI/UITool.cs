using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;



public static class UITool 
{
    
    
    public static VisualElement CreateCardUI(Card card){
        VisualTreeAsset cardTemplate = Resources.Load<VisualTreeAsset>("Card Template");
        VisualTreeAsset runeTemplate = Resources.Load<VisualTreeAsset>("Rune Template");

        VisualElement newCard  = cardTemplate.Instantiate();

        VisualElement illustration = newCard.Q("Sprite");
        VisualElement cardTypesView = newCard.Q("CardTypesView");
        Label title = newCard.Q<Label>("Title");
        Label desc = newCard.Q<Label>("Desc");

        illustration.style.backgroundImage = new StyleBackground(card.illustration);
        title.text = card.title;
        desc.text = card.description;

        CardType[] cardTypes = card.cardTypes;

        foreach(CardType cardType in cardTypes){
            VisualElement newRune = runeTemplate.Instantiate();
            VisualElement icon = newRune.Q("Icon");
            icon.style.backgroundImage = new StyleBackground(cardType.icon);
            // Color iconColor = cardType.color;
            // icon.style.unityBackgroundImageTintColor = iconColor;
            cardTypesView.Add(newRune);
        }
        

        return newCard;
    }

    public static VisualElement CreateCardUIDeck(Card card){ 
        VisualElement newCard = CreateCardUI(card);
        newCard.style.marginBottom = -256;

        return newCard;
    }

   public static Card[] GetRandomCards(int numberOfCard){
        System.Random rnd = new System.Random();
        Card[] allCards = Resources.LoadAll<Card>("Cards");
        Card[] rdnCardList = new Card[numberOfCard];
        for (int i = 0; i < numberOfCard; i++)
        {
            int rdnIndex = rnd.Next(0, allCards.Length);
            rdnCardList[i] = allCards[rdnIndex];
        }   
        return rdnCardList;
   } 
}
