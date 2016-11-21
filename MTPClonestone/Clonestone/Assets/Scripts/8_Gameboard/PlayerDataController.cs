using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

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
            var card = GameboardGameplayController.DrawCard(this);
            card.GetComponent<CardDataController>().Data.CardStatus = CardDataController.CardStatus.inHand;
        }
    }

    void Init()
    {

    }

    void Update()
    {
        if(GameboardInitController.DetermineIfGameIsReady())
        {            
            ///TODO Hole Deck


            
            //Legt die Anzahl der Startkarten fest
            this.SetStartingHandSize();
            
            //Hole Startkarten
            GetStartingHand();

            this.Data.IsReadyPlayer = true;
            
        }


        
    }

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //könnte ein Problem sein, wenn beide gleiczeitig das Spiel starten
        if(players.Length == 1)
            this.isFirstPlayer = true;
        else
            this.isFirstPlayer = false;
    }
}