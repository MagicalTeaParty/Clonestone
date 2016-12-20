using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.IO;

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

        /// <summary>
        /// true, wenn Spieler alle Daten initialisiert hat
        /// </summary>
        public bool IsReadyPlayer;

        public string GamerTag;

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

        /// <summary>
        /// Schaden, den der Spieler bekommt, wenn er versucht aus einem leeren Deck eine Karte zu ziehen
        /// </summary>
        public int Fatigue;
    }

    //FIELDS

    /// <summary>
    /// Liste aller Karten des Spielers unabhängig von deren Aufenthaltsort,
    /// dieser ("CardStatus") ist in jeder Karte gespeichert.
    /// </summary>
    public List<GameObject> CardList;

    internal const int MaxHandSize = 10;
    internal const int MaxMana = 10;
    internal int MaxHealth = 30;

    //Ob bei jeder Karte schon der Besitzer festgelegt wurde
    public bool CardOwnerSetted = false;

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
    
    //METHODS

    void Start()
    {
        //Hier werden die Werte initialisiert
        Data.CurrentMaxMana = 0;
        Data.CurrentActiveMana = Data.CurrentMaxMana;
        Data.CurrentHealth = MaxHealth;
        Data.Fatigue = 0;
    }

    /// <summary>
    /// Setzt das Frontpanel der Karte auf Grün, während sie spielbar (Manakosten) ist
    /// </summary>
    /// <param name="card">Die Karte</param>
    private void ShowCardPlayable(GameObject card)
    {
        if (this.Data.IsActivePLayer && card.GetComponent<CardDataController>().Data.Mana <= Data.CurrentActiveMana && isLocalPlayer && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
        {
            card.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(0, 1, 0, 0.8f);
        }
        else
        {
            card.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    void Update()
    {
        PlayerDataController p1 = null;
        GameObject[] players = GameboardInitController.Players;
        if(players.Length >= 2)
        {
            p1 = players[0].GetComponent<PlayerDataController>();

            if(p1.gameObject != this.gameObject)
            {
                //GEGNERZUG
                //Wenn nicht Host - also nicht "Hager"
                if(this.Data.IsActivePLayer && !this.isLocalPlayer && this.CardList != null)
                {
                    StartCoroutine("playCards");

                    if(Data.CurrentActiveMana == 0)
                    {
                        //Wenn nicht Hager am Zug => Computerzug Endturn
                        if(this.Data.IsActivePLayer && !this.isLocalPlayer && this.CardList != null)
                        {
                            StartCoroutine("CompEndTurnExecuteAfterTime", Random.Range(10, 20));

                        }
                    }

                }
            }
        }

        foreach(GameObject card in this.CardList)
        {
            //Wenn aktiver Spieler UND kartenmana <= spielermana UND karte in Hand
            if(this.Data.IsActivePLayer && card.GetComponent<CardDataController>().Data.Mana <= Data.CurrentActiveMana && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
            {
                card.gameObject.GetComponent<Dragable>().enabled = true;

                if(card.transform.Find("Target").gameObject.active == true)
                    card.transform.Find("Target").gameObject.SetActive(false);
            }
            //Wenn aktiver Spieler UND karte auf Board
            else if(this.Data.IsActivePLayer && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.onBoard)
            {
                card.gameObject.GetComponent<Dragable>().enabled = false;

                if(card.transform.Find("Target").gameObject.activeSelf == false)
                    card.transform.Find("Target").gameObject.SetActive(true);
            }
            //Wenn nicht aktiver Spieler
            //else if(!this.Data.IsActivePLayer && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.onBoard)
            //{
            //    card.gameObject.GetComponent<Dragable>().enabled = false;
            //    if(card.transform.Find("Target").gameObject.active == true)
            //        card.transform.Find("Target").gameObject.SetActive(false);
            //}
            //else if(!this.Data.IsActivePLayer && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
            //{
            //    card.gameObject.GetComponent<Dragable>().enabled = false;
            //    if(card.transform.Find("Target").gameObject.active == true)
            //        card.transform.Find("Target").gameObject.SetActive(false);
            //}


            //if(card.GetComponent<CardDataController>().Data.Mana > Data.CurrentActiveMana && card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
            //{
            //    card.gameObject.GetComponent<Dragable>().enabled = false;
            //}

            ShowCardPlayable(card);
        }
        
        if (!GameboardInitController.DetermineIfGameIsReady() || GameboardDataController.GameState == GameboardDataController.GameStatus.running)
            return;

        if (CardList != null && CardList.Count != 0)
            return;

        //Wenn ich der lokale Spieler bin und den Server hoste
        if (this.isServer && this.isLocalPlayer)
        {
            //SetPlayerOrder(GameboardInitController.Players[0].GetComponent<PlayerDataController>(), GameboardInitController.Players[1].GetComponent<PlayerDataController>());
            //Spielerreihenfolge wird stattdessen hardgecodet

            //Legt die Reihenfolge der Spieler fest
            isFirstPlayer = true;
            Data.IsActivePLayer = true;
        }
        else
        {
            isFirstPlayer = false;
            Data.IsActivePLayer = false;
        }

        //wenn der Spieler noch nicht als "IsReadyPlayer" gesetzt ist:
        if (isServer && !Data.IsReadyPlayer)
        {
            //Hole das Deck und erstelle die Gameobjects
            //!Wichtig, erst nach der Spielerreihenfolge aufrufen!
            getDeckBuilder();

            //Markiere den Spieler als "IsReadyPlayer"
            this.Data.IsReadyPlayer = true;
        }
        
    }

    private IEnumerator playCards()
    {
        foreach(var item in CardList)
        {
            //if(Data.IsReadyPlayer)
            //    return;

            CardDataController cdc = item.GetComponent<CardDataController>();

            if(cdc.Data.CardState == CardDataController.CardStatus.inHand && cdc.Data.Mana <= this.Data.CurrentActiveMana)
            {
                yield return new WaitForSecondsRealtime(Random.Range(3, 10));
                //StartCoroutine("playCard", item);
                if(cdc.Data.CardState == CardDataController.CardStatus.inHand && cdc.Data.Mana <= this.Data.CurrentActiveMana)
                {
                    playCard(item);
                }

            }
        }

        //Wenn nicht Hager am Zug => Computerzug Endturn
        if(this.Data.IsActivePLayer && !this.isLocalPlayer && this.CardList != null)
        {
            StartCoroutine("CompEndTurnExecuteAfterTime", Random.Range(10,20));
            
        }
    }

    IEnumerator CompEndTurnExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if(this.Data.IsActivePLayer && !this.isLocalPlayer && this.CardList != null)
        {
            // Code to execute after the delay
            CompEndTurn();
        }
    }

    private void CompEndTurn()
    {
        GameObject board = GameObject.Find("Board");
        board.GetComponent<GameboardGameplayController>().EndTurn();
    }

    private void playCard(GameObject card)
    {
        GameObject placeToDrop = GameObject.Find("/Board/DropZoneP2Position");
        MoveCard(card, placeToDrop);
        //setze den Status der Karte auf OnBoard
        card.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.onBoard;
        Data.CurrentActiveMana -= card.GetComponent<CardDataController>().Data.Mana;
    }

    private void attackWithCards()
    {
        //GameObject p1cards = GameObject.Find("/Board/DropZoneP1Position");
        //GameObject p2cards = GameObject.Find("/Board/DropZoneP2Position");

        GameObject[] players = GameboardInitController.Players;
        PlayerDataController hager = players[0].GetComponent<PlayerDataController>();
        PlayerDataController comp = players[1].GetComponent<PlayerDataController>();
        
        foreach(var card in comp.CardList)
        {
            if(card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.onBoard)
            {
                int cards = GetLengthOfCardsOnBoard(hager.CardList);
                if(cards > 0)
                {
                    int rand = Random.Range(0, cards);

                    //Die fremde Karte bekommt den Angriff ab
                    card.GetComponent<CardDataController>().Data.Health -= hager.CardList[rand].GetComponent<CardDataController>().Data.Attack;
                    //Die Angriffskarte bekommt die Verteidigung ab
                    hager.CardList[rand].GetComponent<CardDataController>().Data.Health -= card.GetComponent<CardDataController>().Data.Attack;
                }
            }
        }
    }
    
    private int GetLengthOfCardsOnBoard(List<GameObject> cardList)
    {
        int counter = 0;

        foreach(var item in CardList)
        {
            if(item.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.onBoard)
            {
                counter++;
            }
        }

        return counter;
    }

    /// <summary>
    /// Setzt die Anzahl der Karten auf der Starthand fest.
    /// Diese hängt davon ab, ob der Spieler beginnt oder nicht.
    /// </summary>
    public void SetStartingHandSize()
    {
        if (isFirstPlayer)
            //+1 damit der beginnende Spieler auch eine Karte beim ersten Zug "zieht"
            startingHandSize = 3 + 1;
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
            GameObject card = DrawCard();

            if (card != null)
            {
                GameObject pos;
                if (this.isFirstPlayer)
                    pos = GameObject.Find("/Board/Player1HandPosition");
                else
                    pos = GameObject.Find("/Board/Player2HandPosition");

                MoveCard(card, pos);
            }
        }
    }

    /// <summary>
    /// Lädt eine PNG und liefert eine Texture2D
    /// </summary>
    /// <param name="filePath">Pfadangabe zur PNG</param>
    /// <returns></returns>
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }

        return tex;
    }

    //Die folgende Methode ist unnötig, da "Application.dataPath" bereits den gewünschten Pfad zurückliefert
    ///// <summary>
    ///// Ermittelt den Pfad anhand der Plattform: http://answers.unity3d.com/questions/13072/how-do-i-get-the-application-path.html
    ///// </summary>
    ///// <returns></returns>
    ///// 
    //public static string GetApplicationPath()
    //{
    //    //Application.dataPath: https://docs.unity3d.com/ScriptReference/Application-dataPath.html
    //    string path = Application.dataPath;

    //    if (Application.platform == RuntimePlatform.OSXPlayer)
    //        path += "/../../";
    //    else if (Application.platform == RuntimePlatform.WindowsPlayer)
    //        path += "/../";

    //    return path;
    //}

    //Platzhalter für das Karten-Prefab
    public GameObject CardPrefab;
    //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers
    public Transform CardSpawnPosition;
    //Platzhalter für das HeroCard Prefab
    public GameObject HeroCardPrefab;

    public delegate void CreateCards(string text);

    /// <summary>
    /// Zerlegt anhand eines JSON-Arrays die Daten für die Gameobjects
    /// </summary>
    /// <param name="text"></param>
    private void CreateCardsMethod(string text)
    {
        //Da JsonUtility.FromJson nur mit Json-Objekten aber nicht mit Json-Arrays umgehen kann wird zuerst gesplittet um die einzelnen Objekte zu erhalten.
        string[] jsonArray = text.Split(new char[] { '{', '}' });
        string helper;

        foreach (string item in jsonArray)
        {
            //Nach dem Split gibt es auch unnötige Zeichen die wir nicht für das Json-Objekt benötigen. Json-Objekte erkenne wir daran, dass diese mit " beginnen.
            if (item != null && item.Length > 0 && item[0] == '"')
            {
                //Da nach dem Split die {} des Json-Objekts fehlen, werden diese wieder hinzugefügt
                helper = "{" + item + "}";

                //Mittels FromJson wird nun der String der auf helper steht - dieser ist ein Json-Objekt - in ein Objekt vom Typ CardData umgewandelt
                CardDataController.CardData cardData = JsonUtility.FromJson<CardDataController.CardData>(helper);


                //Wenn es sich um eine Hero-Karte handelt...
                if (cardData.TypeName == "Hero")
                {
                    //...wird der HeroSpawnServer aufgerufen,
                    CmdHeroSpawnServer(cardData);

                }
                //...sonst...
                else
                {
                    //wird der CardSpawnServer aktiviert.
                    CmdCardSpawnServer(cardData);
                }
            }
        }

        //Legt die Anzahl der Startkarten fest
        SetStartingHandSize();

        //Hole Startkarten
        GetStartingHand();
    }

    /// <summary>
    /// Platziert die Hero-Karten der Spieler auf das Gameboard
    /// </summary>
    /// <param name="cardData">Die Werte der Karte, die synchronisiert werden</param>
    [Command]
    void CmdHeroSpawnServer(CardDataController.CardData cardData)
    {
        GameObject placeHeroCard;

        if (this.isFirstPlayer)
        {
            placeHeroCard = GameObject.Find("/Board/HeroP1Position");
        }
        else
        {
            placeHeroCard = GameObject.Find("/Board/HeroP2Position");
        }

        #region GameObjekt für die cardData erstellen und cardData dem Hero zuweisen

        GameObject card = (GameObject)Instantiate(HeroCardPrefab, placeHeroCard.transform.position, placeHeroCard.transform.rotation);

        CardDataController cdc = card.GetComponent<CardDataController>();
        cdc.Data = cardData;
        cdc.Data.CardState = CardDataController.CardStatus.onBoard;

        #endregion

        //Bild setzen
        var image = card.GetComponentsInChildren<Image>()[1];

        Texture2D txt2d = LoadPNG(Application.dataPath + @"/Images/Cards/" + cardData.FileName);

        image.sprite = Sprite.Create(txt2d, new Rect(0, 0, txt2d.width, txt2d.height), new Vector2(0.5f, 0.5f));

        //Warnung von Unity: Ausgebessert von LP & TF
        //card.transform.parent = placeHeroCard.transform;
        card.transform.SetParent(placeHeroCard.transform);


        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);
    }

    //Alle serverseitigen Methoden benötigen das Command-Attribut
    //Gerade die Servervariablen senden nur ihre Änderungen an die Clients weiter falls dies in einer Servermethode geschieht.
    /// <summary>
    /// Erzeugt am Server das Gameobject für eine Karte
    /// </summary>
    /// <param name="cardData">Die Werte der Karte, die synchronisiert werden</param>
    [Command]
    void CmdCardSpawnServer(CardDataController.CardData cardData)
    {
        //Anhand der Spielerreihenfolge die Spawnposition bestimmen
        GameObject pos;
        if (this.isFirstPlayer)
            pos = GameObject.Find("/Board/Deck1Position");
        else
            pos = GameObject.Find("/Board/Deck2Position");


        //Mittels Instantiate kann man ein neues GameObject erstellen, in diesem Fall wird das CardPrefab als Vorlage für das GameObject verwendet und an der Position und Ausrichtung von CardSpawnPosition erstellt.        
        GameObject card = (GameObject)Instantiate(CardPrefab, pos.transform.position, pos.transform.rotation);

        //Übergabeparameter cardData auf die Instanz card speichern
        CardDataController cdc = card.GetComponent<CardDataController>();

        cdc.Data = cardData;

        if (this.CardList == null)
            this.CardList = new List<GameObject>();

        this.CardList.Add(card);

        // Get all components of type Image that are children of this GameObject.
        Image image = card.GetComponentsInChildren<Image>()[1];

        Texture2D txt2d = LoadPNG(Application.dataPath + @"/Images/Cards/" + cardData.FileName);

        image.sprite = Sprite.Create(txt2d, new Rect(0, 0, txt2d.width, txt2d.height), new Vector2(0.5f, 0.5f));

        #region Testladen eines Bildes

        //Image im = GetComponentsInChildren<Image>()[1];
        //Texture2D to Image: http://answers.unity3d.com/questions/650552/convert-a-texture2d-to-sprite.html

        #endregion

        card.transform.SetParent(pos.transform);

        //Mittels NetworkServer.SpawnWithClientAuthority kann man ein GameObject - in diesem Fall die Karte (card) - über das Netzwerk bekannt machen und auch einen Besitzer festlegen.
        //connectionToClient besitzt die Daten von dem aktuellen Spieler der die Karte erzeugt hat, somit "gehört" (isAuthority) die Karte dem aktuellen Spieler der diese Methode aufgerufen hat
        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);
    }

    /// <summary>
    /// Holt ein Deck und erstelle die Gameobjects.
    /// !Wichtig die Methode erst nach Festlegen der Spielerreihenfolge aufrufen!
    /// </summary>
    public void getDeckBuilder()
    {
        CreateCards _CreateCardsReceiver = CreateCardsMethod;

        StartCoroutine("getDeck", _CreateCardsReceiver);
    }

    /// <summary>
    /// Holt das Deck aus der Datenbank.
    /// </summary>
    /// <param name="_CreateCardsReceiver"></param>
    /// <returns></returns>
    private IEnumerator getDeck(CreateCards _CreateCardsReceiver)
    {
        //Mittels JsonUtility.FromJson kann man ein JSON-Objekt auf ein C# Objekt mappen/umwandeln.        
        //CardDataController.CardData data = JsonUtility.FromJson<CardDataController.CardData>(jsonstring);

        //Beispiel für ein Json-Objekt
        //{\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }

        string url = "http://mtp.a-k-t.at/clonestone/Deck/GetDeck";
        WWWForm form = new WWWForm();
        form.AddField("idDeck", 1);

        WWW returnJson = new WWW(url, form);

        yield return returnJson;

        _CreateCardsReceiver(returnJson.text);
    }

    /// <summary>
    /// Bewegt eine Karte zwischen Parent-Elementen in der Unity-Hierarchy
    /// </summary>
    /// <param name="card">Die Karte, die bewegt wird</param>
    /// <param name="placeToDrop">Die Zone, in der die Karte abgelegt wird</param>
    public void MoveCard(GameObject card, GameObject placeToDrop)
    {
        //heroPosition = Dropbereich der Hero-Karte
        //if (placeToDrop == heroPosition)
        //{
        //    card.GetComponent<LayoutElement>().enabled = false;
        //}

        //Wenn keine Karte zurückgeliefert wird, muss das Deck leer sein, ...
        if (card == null)
        {
            //...daher erhöht sich die Fatigue.
            Data.Fatigue++;
            return;
        }

        //Hier werden die Karten auf den DiscardPile gelegt
        if (card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inDiscardPile)
        {
            if (GameboardInitController.Players[0].GetComponent<PlayerDataController>().Data.IsActivePLayer)
                placeToDrop = GameObject.Find("/Board/DiscardPile1");
            else
                placeToDrop = GameObject.Find("/Board/DiscardPile2");
        }

        card.transform.SetParent(placeToDrop.transform);
        card.SetActive(true);
    }

    /// <summary>
    /// Diese Methode zieht eine Karte vom Deck des Spielers.
    /// Soll mehr als eine Karte gezogen werden, muss die Methode dementsprechend oft aufgerufen werden
    /// </summary>
    /// <returns>Die gezogene Karte</returns>
    public GameObject DrawCard()
    {
        if (CardList.Count < 1)
            return null;

        GameObject cardDrawn = null;

        ///Wird benötigt, um die Anzahl der Karten in der Hand zu bestimmen.
        int cardsInHandCount = 0;

        ///Die Schleife sucht in der Kartenliste des Spielers die erste Karte, deren "CardStatus" gleich "inDeck" ist, und gibt diese zurück
        foreach (GameObject card in CardList)
        {
            //Zählt die Anzahl der Karten in der Hand
            if (card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
                cardsInHandCount += 1;

            if (card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inDeck)
            {
                cardDrawn = card;

                if (cardsInHandCount < PlayerDataController.MaxHandSize)
                    cardDrawn.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.inHand;
                //Wenn die Anzahl der Karten in der Hand 10 ist, wird jede weitere gezogene Karte als "inDiscardPile" markiert
                else
                    cardDrawn.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.inDiscardPile;

                return cardDrawn;
            }
        }

        return cardDrawn;
    }
}