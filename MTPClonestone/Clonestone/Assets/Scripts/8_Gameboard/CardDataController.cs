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


    public Text LifeField;
    public Text ManaField;
    public Text LifeMinion;
    int heroIndex;

    int currentLife;
    int currentMana;
    int minionLife;

    /// <summary>
    /// Besitzer der Karte
    /// </summary>
    public GameObject Owner;

    private void Start()
    {
        //if ( .GetComponent<NetworkIdentity>().isLocalPlayer)


        GameObject placeHeroCard1 = GameObject.Find("/Board/HeroP1Position/HeroCard(Clone)");

        if(placeHeroCard1 == this.gameObject)
        {
            Debug.Log("Hero 1");
            this.heroIndex = 0;
        }

        GameObject placeHeroCard2 = GameObject.Find("/Board/HeroP2Position/HeroCard(Clone)");

        if (placeHeroCard2 == this.gameObject)
        {
            Debug.Log("Hero 2");
            this.heroIndex = 1;
        }

        if (this.Data.TypeName == "Hero")
        {
            LifeField = this.gameObject.GetComponentsInChildren<Text>()[0];
            Debug.Log(LifeField.text);
            ManaField = this.gameObject.GetComponentsInChildren<Text>()[1];
        }

        if (this.Data.TypeName != "Hero")
        {
            LifeMinion = this.gameObject.GetComponentInChildren<Text>();
        }
        
    }

    /// <summary>
    /// Legt den Besitzer der Karte fest
    /// </summary>
    private void setOwner()
    {
        //Hole alle Spieler
        var players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length < 2)
        {
            //Debug.Log("ERROR: setOwner()");
            return;
        }

        //if(players[0].GetComponent<PlayerDataController>().CardOwnerSetted == true &&
        //    players[1].GetComponent<PlayerDataController>().CardOwnerSetted == true)
        //{
        //    //Debug.Log("ERROR: setOwner()");
        //    return;
        //}

        var cards = players[0].GetComponent<PlayerDataController>().CardList;
        foreach(var card in cards)
        {
            if(card == this.gameObject)
            {
                this.Owner = players[0];
            }
        }

        cards = players[1].GetComponent<PlayerDataController>().CardList;
        foreach(var card in cards)
        {
            if(card == this.gameObject)
            {
                this.Owner = players[1];
            }
        }

        players[0].GetComponent<PlayerDataController>().CardOwnerSetted = true;
        players[1].GetComponent<PlayerDataController>().CardOwnerSetted = true;

        //Debug.Log("OK: setOwner()");
    }


    private void Update()
    {

        setOwner();

        if (this.Data.TypeName == "Hero")
        {
            currentLife = GameboardInitController.Players[heroIndex].GetComponent<PlayerDataController>().Data.CurrentHealth;
            LifeField.text = currentLife.ToString();
            //Debug.Log(LifeField.text);
            currentMana = GameboardInitController.Players[heroIndex].GetComponent<PlayerDataController>().Data.CurrentActiveMana;
            ManaField.text = currentMana.ToString();

        }
               
        if (this.Data.TypeName != "Hero")
        {
            minionLife = this.Data.Health;
            LifeMinion.text = minionLife.ToString();

            checkAlive();
        }

        setCardVisibility();
    }

    /// <summary>
    /// Prüft ob die Karte noch genug leben hat um am Brett bleiben zu drüfen, wenn nicht wird der Status auf inDiscardPile geändert
    /// </summary>
    private void checkAlive()
    {
        if(this.Data.Health <= 0 && this.Data.Health > int.MinValue)
        {
            //Debug.Log("Card " + this.Data.CardName + " destroyed");
            this.Data.CardState = CardStatus.inDiscardPile;

            MoveOnDiscardPile();

            this.Data.Health = int.MinValue;

            //Hero ein Leben abziehen
            this.Owner.GetComponent<PlayerDataController>().Data.CurrentHealth--;
        }
    }

    private void setCardVisibility()
    {
        if (this.Data.TypeName == "Hero")
            return;
                
        var cardBackGameObjekt = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        var cardLifeGameObjekt = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //Debug.Log(this.GetComponent<CardDataController>().Data.IdCard);

        var players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length < 2)
            return;

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
                cardBackGameObjekt.SetActive(false);
                cardLifeGameObjekt.SetActive(true);
                break;
            case CardStatus.inDiscardPile:
                //Debug.Log("Card " + this.Data.CardName + " discarded");
                //GameObject.Destroy(this); //Vernichte das GameObject
                
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Hier wird die Karte auf den Ablagestapel verschoben
    /// </summary>
    private void MoveOnDiscardPile()
    {
        //Für Testzwecke wird die Karte, egal von welchem Spieler auf den Ablagestapel von Spieler 1 verschoben
        GameObject pile = GameObject.Find("DiscardPile1");

        this.gameObject.transform.SetParent(pile.transform);
        
    }
}