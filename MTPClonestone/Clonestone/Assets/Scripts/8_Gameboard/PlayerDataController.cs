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

        public bool IsReadyPlayer; //true, wenn Spieler alle Daten initialisiert hat
       
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
        //p1.isFirstPlayer = GameboardDataController.TossCoin();
        p1.isFirstPlayer = true;
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
            GameObject card = GameboardGameplayController.DrawCard(this);
            //Folgender Code auskommentiert, weil besser in der Methode "DrawCard" selbst bereits der CardState auf "inHand" geändert wird.
            //card.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.inHand;

            GameObject pos;
            if (this.isFirstPlayer)
            {
                pos = GameObject.Find("/Board/Player1HandPosition");
               
            }
            else
            {
                pos = GameObject.Find("/Board/Player2HandPosition");
                
            }

            MoveCard(card, pos);
    
        }
   }

    /// <summary>
    /// Lädt ein PNG und liefert eine Texture2D
    /// </summary>
    /// <param name="filePath"></param>
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
    
    /// <summary>
    /// Ermittelt den Pfad anhand der Plattform: http://answers.unity3d.com/questions/13072/how-do-i-get-the-application-path.html
    /// </summary>
    /// <returns></returns>
    /// 
    public static string GetApplicationPath()
    {
        //Application.dataPath: https://docs.unity3d.com/ScriptReference/Application-dataPath.html
        string path = Application.dataPath;

        if (Application.platform == RuntimePlatform.OSXPlayer)

        {
            path += "/../../";
        }

        else if (Application.platform == RuntimePlatform.WindowsPlayer)

        {
            path += "/../";
        }

        return path;
}

    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

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

    /// <summary>
    /// Holt ein Deck und erstelle die Gameobjects. !Wichtig die Methode erst nach Festlegen der Spielerreihenfolge aufrufen!
    /// </summary>
    public void getDeckBuilder()
    {
        CreateCards _CreateCardsReceiver = CreateCardsMethod;

        StartCoroutine("getDeck", _CreateCardsReceiver);
    }

    private IEnumerator getDeck(CreateCards _CreateCardsReceiver)
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

        yield return returnJson;

        _CreateCardsReceiver(returnJson.text);
        
    }

    //Alle serverseitigen Methoden benötigen das Command-Attribut
    //Gerade die Servervariablen senden nur ihre Änderungen an die Clients weiter falls dies in einer Servermethode geschieht.
    /// <summary>
    /// Erzeugt am Server das Gameobject für eine Karte
    /// </summary>
    /// <param name="cardData"></param>
    [Command]
    void CmdCardSpawnServer(CardDataController.CardData cardData)
    {
        //Anhand der Spielernummer die Spawnposition bestimmen
        GameObject pos;
        if(this.isFirstPlayer)
        {
            pos = GameObject.Find("/Board/Deck1Position");
        }
        else
        {
            pos = GameObject.Find("/Board/Deck2Position");
        }
            
        //Mittels Instantiate kann man ein neues GameObject erstellen, in diesem Fall wird das CardPrefab als Vorlage für das GameObject verwendet und an der Position und Ausrichtung von CardSpawnPosition erstellt.        
        GameObject card = (GameObject)Instantiate(CardPrefab, pos.transform.position, CardSpawnPosition.rotation);

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
        var image = card.GetComponentsInChildren<Image>()[1];

        Texture2D txt2d = LoadPNG(GetApplicationPath() + @"/Images/Cards/" + cardData.FileName);

                
        image.sprite = Sprite.Create(txt2d, new Rect(0, 0, txt2d.width, txt2d.height), new Vector2(0.5f, 0.5f));
        
        #region Testladen eines Bildes
        
        //Image im = GetComponentsInChildren<Image>()[1];        //Texture2D to Image: http://answers.unity3d.com/questions/650552/convert-a-texture2d-to-sprite.html
        

        #endregion


    }

    void Update()
    {
        if (!GameboardInitController.DetermineIfGameIsReady())
            return;

        if (CardList != null && CardList.Count != 0)
            return;

        //Legt die Reihenfolge der Spieler fest   
             
        SetPlayerOrder(GameboardInitController.Players[0].GetComponent<PlayerDataController>(), GameboardInitController.Players[1].GetComponent<PlayerDataController>());

        //Hole das Deck und erstelle die Gameobjects - wichtig, erst nach der Spielerreihenfolge aufrufen
        getDeckBuilder();

        //Legt die Anzahl der Startkarten fest
        SetStartingHandSize();

        //Hole Startkarten
        GetStartingHand();
    }


    /// <summary>
    /// Bewegt Card Objekte zwischen "Parent-Elementen"
    /// </summary>
    /// <param name="card">Kartenobjekt</param>
    /// <param name="placeToDrop">Zone in der die Karte abgelegt wird</param>
    public void MoveCard(GameObject card, GameObject placeToDrop)
    {
        //heroPosition = Dropbereich der Hero-Karte
        //if (placeToDrop == heroPosition)
        //{
        //    card.GetComponent<LayoutElement>().enabled = false;
        //}

        card.transform.parent = placeToDrop.transform;
        card.SetActive(true);

    }

}