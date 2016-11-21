using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class PlayerDataController : NetworkBehaviour
{
    /// <summary>
    /// Wird als Struktur implementiert, um das Attribut [SyncVar] erhalten zu können.
    /// </summary>
    public struct PlayerData
    {
        /// <summary>
        /// True: Spieler ist am Zug
        /// False: Spieler ist nicht am Zug
        /// </summary>
        public bool IsActivePLayer;
       
        public string GamerTag;

        //public string LoginName;
        //public int HandSize;

        /// <summary>
        /// Momentan maximaler Manavorrat
        /// </summary>
        public int CurrentMaxMana;

        /// <summary>
        /// Aktuell verfügbares Mana
        /// </summary>
        public int CurrentActiveMana;

        /// <summary>
        /// Aktuelle Lebenspunkte des Spielers
        /// </summary>
        public int CurrentHealth;
    }

    //Fields

    /// <summary>
    /// Liste aller Karten des Spielers unabhängig von deren Aufenthaltsort,
    /// dieser ("CardStatus") ist in jeder Karte gespeichert.
    /// </summary>
    public List<GameObject> CardList;

    const int MaxHandSize = 10;
    const int MaxMana = 10;
    const int MaxHealth = 30;

    /// <summary>
    /// Die Anzahl der Karten, die der Spieler zu Beginn des Spiels auf die Hand bekommt.
    /// </summary>
    private int startingHandSize;

    /// <summary>
    /// Gibt an ob der Spieler beginnt oder nicht
    /// </summary>
    private bool isFirstPlayer;

    [SyncVar]
    public PlayerData Data;

    //Methods

    /// <summary>
    /// Ändert die Bool-Variable "IsActivePlayer" von "true" auf "false" oder vice versa.
    /// </summary>
    public void ChangeIsActivePlayer()
    {
        Data.IsActivePLayer = !Data.IsActivePLayer;
    }

    /// <summary>
    /// Setzt die Variable "isFirstPlayer" für beide Spieler entsprechend der Rückgabe von Methode "TossCoin()"
    /// </summary>
    /// <param name="p1">Spieler 1</param>
    /// <param name="p2">Spieler 2</param>
    public static void SetPlayerOrder(PlayerDataController p1, PlayerDataController p2)
    {
        p1.isFirstPlayer = GameboardDataController.TossCoin();
        p2.isFirstPlayer = !p1.isFirstPlayer;
    }

    /// <summary>
    /// Setzt die Anzahl der Karten auf der Starthand fest.
    /// Diese hängt davon ab, ob der Spieler beginnt oder nicht.
    /// </summary>
    public void SetStartingHandSize()
    {
        if (isFirstPlayer)
            startingHandSize = 3;
        else startingHandSize = 4;
    }

    /// <summary>
    /// Ruft die Methode "DrawCard" auf.
    /// Anzahl der Aufrufe hängt von der "startingHandSize" ab.
    /// </summary>
    public void GetStartingHand()
    {
        for (int i = 0; i < this.startingHandSize; i++)
        {
            GameboardGameplayController.DrawCard(this);
        }
    }

    void Start()
    {
        //if (!GameboardDataController.IsRunningGame)
        //    return;

        ///TODO Alle Anfänglichen Initialisierungen


        ///TODO Hole Deck
        getDeckBuilder(gameObject);

        //Legt die Reihenfolge der Spieler fest
        SetPlayerOrder(GameboardInitController.Players[0].GetComponent<PlayerDataController>(), GameboardInitController.Players[1].GetComponent<PlayerDataController>());

        //Legt die Anzahl der Startkarten fest
        SetStartingHandSize();

        //Hole Startkarten
        GetStartingHand();
    }

    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

    public void getDeckBuilder(GameObject player)
    {
        StartCoroutine("getDeck", player);
    }

    private IEnumerator getDeck()
    {
        //Mittels JsonUtility.FromJson kann man ein JSON-Objekt auf ein C# Objekt mappen/umwandeln.        
        //CardDataController.CardData data = JsonUtility.FromJson<CardDataController.CardData>(jsonstring);

        //Beispiel für ein Json-Objekt
        //{\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }

        string url = "http://localhost:53861/Deck/GetDeck";
        WWWForm form = new WWWForm();
        form.AddField("idDeck", 1);

        WWW returnJson = new WWW(url, form);
        Debug.Log("hallo");

        yield return returnJson.text;


        //Da JsonUtility.FromJson nur mit Json-Objekten aber nicht mit Json-Arrays umgehen kann wird zuerst gesplittet um die einzelnen Objekte zu erhalten.
        string[] jsonArray = returnJson.text.Split(new char[] { '{', '}' });
        string helper;

        foreach (var item in jsonArray)
        {
            //Nach dem Split gibt es auch unnötige Zeichen die wir nicht für das Json-Objekt benötigen. Json-Objekte erkenne wir daran, dass diese mit " beginnen.
            if (item[0] == '"')
            {
                //Da nach dem Split die {} des Json-Objekts fehlen, werden diese wieder hinzugefügt
                helper = "{" + item + "}";

                //Mittels FromJson wird nun der String der auf helper steht - dieser ist ein Json-Objekt - in ein Objekt vom Typ CardData umgewandelt
                CardDataController.CardData cardData = JsonUtility.FromJson<CardDataController.CardData>(helper);

                //If Herocard, dann
                if (cardData.TypeName == "Hero")
                {
                    #region GameObjekt für die cardData erstellen und cardData dem Hero zuweisen

                    ///TODO erstellen eines Heros  

                    #endregion
                }
                else //else,.... (wenn nicht hero)
                {
                    #region GameObjekt für die cardData erstellen und cardData zuweisen

                    CmdCardSpawnServer(cardData);

                    #endregion
                }
            }
        }

        Debug.Log("hallo");

    }

    //Alle serverseitigen Methoden benötigen das Command-Attribut
    //Gerade die Servervariablen senden nur ihre Änderungen an die Clients weiter falls dies in einer Servermethode geschieht.
    [Command]
    void CmdCardSpawnServer(CardDataController.CardData cardData)
    {
        //Mittels Instantiate kann man ein neues GameObject erstellen, in diesem Fall wird das CardPrefab als Vorlage für das GameObject verwendet und an der Position und Ausrichtung von CardSpawnPosition erstellt.        
        GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.position, CardSpawnPosition.rotation);

        //Übergabeparameter cardData auf die Instanz card speichern
        CardDataController cdc = card.GetComponent<CardDataController>();

        //cdc.Data.CardName = cardData.CardName;

        cdc.Data = cardData;
        

        //Mittels NetworkServer.SpawnWithClientAuthority kann man ein GameObject - in diesem Fall die Karte (card) - über das Netzwerk bekannt machen und auch einen Besitzer festlegen.
        //connectionToClient besitzt die Daten von dem aktuellen Spieler der die Karte erzeugt hat, somit "gehört" (isAuthority) die Karte dem aktuellen Spieler der diese Methode aufgerufen hat
        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);

        if (this.CardList == null)
            this.CardList = new List<GameObject>();

        this.CardList.Add(card);


        // Get all components of type Image that are children of this GameObject.
        var images = card.GetComponentsInChildren<Image>();


        // Loop through each image and set it's Sprite to the other Sprite.
        foreach (Image image in images)
        {   
            ///TODO richter Befehl für Sprite
            
            //image.sprite = Sprite.Create(Resources.Load<Texture2D>(@"Assets/Images/Cards/" + cardData.FileName), image.sprite.rect, image.sprite.pivot);
        }

    }

    void Update()
    {
        //foreach(var c in this.CardList)
        //{
        //    Debug.Log(c.GetComponent<CardDataController>().Data.CardName);
        //}
    }

}