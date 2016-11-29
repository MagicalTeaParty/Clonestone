﻿using UnityEngine;

public class GameboardGameplayController : MonoBehaviour
{
    //Fields
    TimerScript timer;

    bool gameOver = false;

    //Methods

    void Start()
    {
        timer = GameObject.Find("/Board/EndTurn/EndTurnButton").GetComponent<TimerScript>();
    }

    void Update()
    {
        if (!GameboardInitController.DetermineIfGameIsRunning())
            return;

        ///TODO Check if game is won
        for (int i = 0; i < GameboardInitController.Players.Length; i++)
        {
            Debug.Log("Player " + i + ": " + GameboardInitController.Players[i].GetComponent<PlayerDataController>().Data.CurrentHealth);

            if (GameboardInitController.Players[i].GetComponent<PlayerDataController>().Data.CurrentHealth <= 0 && gameOver == false)
            {
                int winHelper;
                if (i == 0) winHelper = 1;
                else winHelper = 0;

                ///TODO Do something when game is won
                Debug.Log("GAME OVER!" + "Player " + winHelper + " has won");
                gameOver = true;
            }
        }
    }

    /// <summary>
    /// Diese Methode zieht eine Karte vom Deck des mitgegebenen Spielers.
    /// Soll mehr als eine Karte gezogen werden, muss die Methode dementsprechend oft aufgerufen werden
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static GameObject DrawCard(PlayerDataController player)
    {
        if (player.CardList.Count < 1)
        {
            return null;
        }

        GameObject cardDrawn = null;
        int cardsInHandCount = 0;

        ///Die Schleife sucht in der Kartenliste des mitgegebenen Spielers die erste Karte, deren "CardStatus" gleich "inDeck" ist, und gibt diese zurück
        foreach (GameObject card in player.CardList)
        {
            //Zählt die Anzahl der Karten in der Hand
            if (card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inHand)
                cardsInHandCount += 1;

            if (card.GetComponent<CardDataController>().Data.CardState == CardDataController.CardStatus.inDeck)
            {
                cardDrawn = card;

                if (cardsInHandCount < PlayerDataController.MaxHandSize)
                {
                    cardDrawn.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.inHand;
                }
                //Wenn die Anzahl der Karten in der Hand 10 ist, wird jede weitere gezogene Karte als "inDiscardPile" markiert
                else
                {
                    cardDrawn.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.inDiscardPile;
                }
                return cardDrawn;
            }
        }

        return cardDrawn;
    }

    /// <summary>
    /// Diese Methode wechselt den Zustand des Bools "IsActivePlayer" der mitgegebenen Spieler
    /// </summary>
    /// <param name="player"></param>
    public static void ChangeActivePlayer(GameObject player1, GameObject player2)
    {
        player1.GetComponent<PlayerDataController>().ChangeIsActivePlayer();
        player2.GetComponent<PlayerDataController>().ChangeIsActivePlayer();
    }

    /// <summary>
    /// Beendet den Zug: Beendet/Startet den Timer, erhöht und füllt das Mana, zieht eine Karte
    /// </summary>
    public void EndTurn()
    {
        //Beende den Timer und Setze die Zeit zurück
        timer.StopCoroutine("CountDown");
        timer.TimeLeft = TimerScript.time4Round;

        //Finde beide Spieler und speichere sie in ein Array
        GameObject[] players = GameboardInitController.Players;

        if (players.Length < 2)
            return;

        //Wechsle den aktiven Spieler
        ChangeActivePlayer(players[0], players[1]);

        GameObject card = null;
        GameObject placeToDrop = null;

        //Wenn players[0] aktiv ist
        if (players[0].GetComponent<PlayerDataController>().Data.IsActivePLayer)
        {
            //Fülle sein Mana auf
            RefillMana(players[0]);
            //Ziehe eine Karte für ihn
            card = DrawCard(players[0].GetComponent<PlayerDataController>());

            placeToDrop = GameObject.Find("/Board/Player1HandPosition");
            players[0].GetComponent<PlayerDataController>().MoveCard(card, placeToDrop);
            
            //Deal Fatigue
            players[0].GetComponent<PlayerDataController>().Data.CurrentHealth -= players[0].GetComponent<PlayerDataController>().Data.Fatigue;
        }
        //Wenn players[1] aktiv ist
        else
        {
            //Fülle ihr Mana auf
            RefillMana(players[1]);
            //Ziehe eine Karte für sie
            card = DrawCard(players[1].GetComponent<PlayerDataController>());

            placeToDrop = GameObject.Find("/Board/Player2HandPosition");
            players[1].GetComponent<PlayerDataController>().MoveCard(card, placeToDrop);

            //Deal Fatigue
            players[1].GetComponent<PlayerDataController>().Data.CurrentHealth -= players[1].GetComponent<PlayerDataController>().Data.Fatigue;
        }

        //Starte den Timer
        timer.StartCoroutine("CountDown");
    }

    void RefillMana(GameObject player)
    {
        player.GetComponent<PlayerDataController>().Data.CurrentMaxMana += 1;
        player.GetComponent<PlayerDataController>().Data.CurrentActiveMana = player.GetComponent<PlayerDataController>().Data.CurrentMaxMana;
    }
}