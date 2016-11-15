using UnityEngine;
using System.Collections;

public class CardDataController : MonoBehaviour
{
    public enum CardStatus { inDeck, inHand, onBoard, inDiscardPile }


    //Fields
    public struct CardData
    {
        public int IdDeck;
        public string DeckName;

        public int IdClass;
        public string Class;

        public int IdType;
        public string TypeName;

        public int IdCard;
        public string CardName;
        public int Mana;
        public int Attack;
        public int Health;
        public string Flavour;

        public int IdDeckCard;
        public string FileName;
        
        public Transform Transform;
        public CardStatus CardStatus;
    }
}
