using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardDataController : NetworkBehaviour
{
    public enum CardStatus { inDeck, inHand, onBoard, inDiscardPile }

    /// <summary>
    /// Wird als Struktur implementiert, um das Attribut [SyncVar] erhalten zu können.
    /// </summary>
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
        public int MaxAttack;
        public int Health;
        public int MaxHealth;

        public string Flavour;

        public int IdDeckCard;
        public string FileName;
        
        public Transform Transform;
        public CardStatus CardState;
    }

    //Fields
    [SyncVar]
    public CardData Data;


    public Transform LifeField;


    private void Update()
    {
        setCardVisibility();

        LifeField.GetComponent<Text>().text = GameboardInitController.Players[0].GetComponent<PlayerDataController>().Data.CurrentHealth.ToString();
    }


    private void setCardVisibility()
    {
        var cardBackGameObjekt = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        var cardLifeGameObjekt = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //Debug.Log(this.GetComponent<CardDataController>().Data.IdCard);

        var players = GameObject.FindGameObjectsWithTag("Player");
        
        GameObject owner=null;

        var cards = players[0].GetComponent<PlayerDataController>().CardList;
        foreach (var card in cards)
        {
            if(card == this.gameObject)
            {
                owner = players[0];
            }
        }

        cards = players[1].GetComponent<PlayerDataController>().CardList;
        foreach (var card in cards)
        {
            if (card == this.gameObject)
            {
                owner = players[1];
            }
        }

        switch (this.Data.CardState)
        {
            case CardStatus.inDeck:
                cardBackGameObjekt.SetActive(true);
                cardLifeGameObjekt.SetActive(false);
                break;
            case CardStatus.inHand:
                if (owner.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    cardBackGameObjekt.SetActive(false);
                    cardLifeGameObjekt.SetActive(true);
                }
                else
                {
                    cardBackGameObjekt.SetActive(true);
                }

                break;
            case CardStatus.onBoard:
                cardLifeGameObjekt.SetActive(true);
                break;
            case CardStatus.inDiscardPile:
                break;
            default:
                break;
        }
    }
}