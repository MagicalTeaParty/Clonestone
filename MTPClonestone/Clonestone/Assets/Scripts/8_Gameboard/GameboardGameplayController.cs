using UnityEngine;

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
                GameboardDataController.GameState = GameboardDataController.GameStatus.ending;
                InfoTextController info = new InfoTextController();
                info.showInfoText("GAME OVER!" + "Player " + winHelper + " has won", 1);
                gameOver = true;
            }
        }
    }

    /// <summary>
    /// Diese Methode zieht eine Karte vom Deck des mitgegebenen Spielers.
    /// Soll mehr als eine Karte gezogen werden, muss die Methode dementsprechend oft aufgerufen werden
    /// </summary>
    /// <param name="p">Der Spieler, der die Karte bekommt</param>
    /// <returns>Die gezogene Karte</returns>
    public static GameObject DrawCard(PlayerDataController p)
    {
        if (p.CardList.Count < 1)
        {
            return null;
        }

        GameObject cardDrawn = null;

        ///Wird benötigt, um die Anzahl der Karten in der Hand zu bestimmen.
        int cardsInHandCount = 0;

        ///Die Schleife sucht in der Kartenliste des mitgegebenen Spielers die erste Karte, deren "CardStatus" gleich "inDeck" ist, und gibt diese zurück
        foreach (GameObject card in p.CardList)
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
    /// <param name="p1">Spieler 1</param>
    /// <param name="p2">Spieler 2</param>
    public static void ChangeActivePlayer(PlayerDataController p1, PlayerDataController p2)
    {
        p1.ChangeIsActivePlayer();
        p2.ChangeIsActivePlayer();
    }

    /// <summary>
    /// Beendet den Zug:
    /// - beendet den Countdown
    /// - wechselt den aktiven Spieler
    /// - erhöht und füllt das Mana
    /// - zieht eine Karte
    /// - rechnet Fatigue ab
    /// - bewegt die Karte
    /// - startet den Countdown
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
        PlayerDataController p1 = players[0].GetComponent<PlayerDataController>();
        PlayerDataController p2 = players[1].GetComponent<PlayerDataController>();

        //Wechsle den aktiven Spieler
        ChangeActivePlayer(p1, p2);

        GameObject card = null;
        GameObject placeToDrop = null;

        //Wenn players[0] aktiv ist
        if (p1.Data.IsActivePLayer)
        {
            //Fülle sein Mana auf
            RefillMana(p1);
            //Ziehe eine Karte für ihn
            card = DrawCard(p1);
            if (card == null)
                DealFatigue(p1);

            placeToDrop = GameObject.Find("/Board/Player1HandPosition");
            p1.MoveCard(card, placeToDrop);
        }
        //Wenn players[1] aktiv ist
        else
        {
            //Fülle ihr Mana auf
            RefillMana(p2);
            //Ziehe eine Karte für sie
            card = DrawCard(p2);
            if (card == null)
                DealFatigue(p2);

            placeToDrop = GameObject.Find("/Board/Player2HandPosition");
            p2.MoveCard(card, placeToDrop);
        }

        //Starte den Timer
        timer.StartCoroutine("CountDown");
    }

    void RefillMana(PlayerDataController p)
    {
        if (p.Data.CurrentMaxMana < PlayerDataController.MaxMana)
            p.Data.CurrentMaxMana += 1;

        p.Data.CurrentActiveMana = p.Data.CurrentMaxMana;
    }

    void DealFatigue(PlayerDataController p)
    {
        p.Data.CurrentHealth -= p.Data.Fatigue;
    }
}