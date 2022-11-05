using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
   public enum Rarity {Commun,Rare,Legendary};
   public CardType[] cardTypes; 

   public enum MasterCardTypeList { Action, Upgarde, Passive };
	public MasterCardTypeList masterCardType;

   public string title;
   public string description;
   public Rarity rarity = new Rarity();
   public Sprite illustration;
   // public Script script;
}
