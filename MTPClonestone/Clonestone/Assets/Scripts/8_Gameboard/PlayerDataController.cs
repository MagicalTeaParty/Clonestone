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
    }

    //Fields

    /// <summary>
    /// Liste aller Karten des Spielers unabhängig von deren Aufenthaltsort,
    /// dieser ("CardStatus") ist in jeder Karte gespeichert.
    /// </summary>
    public List<GameObject> CardList;

    public const int MaxHandSize = 10;
    public const int MaxMana = 10;

    /// <summary>
    /// Die Anzahl der Karten, die der Spieler zu Beginn des Spiels auf die Hand bekommt.
    /// </summary>
    private int startingHandSize;

    private bool isFirstPlayer;

    [SyncVar]
    public PlayerData Data;

    //Methods

    /// <summary>
    /// Ändert die Bool-Variable "IsActivePlayer" von "true" auf "false" oder vice versa.
    /// </summary>
    public void PlayerChange()
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
    /// <param name="player">Das Spielerobjekt des Clients</param>
    public void GetStartingHand(GameObject player)
    {
        for (int i = 0; i < player.GetComponent<PlayerDataController>().startingHandSize; i++)
        {
            GameboardGameplayController.DrawCard(player);
        }
    }
}