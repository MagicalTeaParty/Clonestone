using UnityEngine;

public class GameboardGameplayController : MonoBehaviour
{
    //Fields
    GameObject info;
    TimerScript timer;

    //Methods

    void Start()
    {
        info = GameObject.Find("/Board/Info");
        timer = GameObject.Find("/Board/EndTurn/EndTurnButton").GetComponent<TimerScript>();
    }

    void Update()
    {
        if (GameboardDataController.GameState != GameboardDataController.GameStatus.running)
            return;

        //Hier wird überprüft, ob das Spiel gewonnen wurde
        CheckWinCondition(GameboardInitController.Players);
    }

    /// <summary>
    /// Diese Methode wechselt den Zustand des Bools "IsActivePlayer" der mitgegebenen Spieler von "true" auf "false" oder vice versa.
    /// </summary>
    /// <param name="p1">Spieler 1</param>
    /// <param name="p2">Spieler 2</param>
    public static void ChangeActivePlayer(PlayerDataController p1, PlayerDataController p2)
    {
        p1.Data.IsActivePLayer = !p1.Data.IsActivePLayer;
        p2.Data.IsActivePLayer = !p1.Data.IsActivePLayer;
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
    internal void EndTurn()
    {
        //Hier wird RenderMode geändert damit Karten während Drag&Drop sichtbar sind
        GameObject boardCanvas;
        boardCanvas = GameObject.Find("/Board");
        boardCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        //Beende den Timer und Setze die Zeit zurück
        timer.StopCoroutine("CountDown");
        timer.TimeLeft = TimerScript.time4Round;

        //Hole beide Spieler aus der Initialisierung und speichere sie in ein Array
        GameObject[] players = GameboardInitController.Players;
        if (players.Length < 2)
            return;
        PlayerDataController p1 = players[0].GetComponent<PlayerDataController>();
        PlayerDataController p2 = players[1].GetComponent<PlayerDataController>();

        //Wechsle den aktiven Spieler...
        ChangeActivePlayer(p1, p2);
        //und informiere die Spieler
        ShowInfoTurnText(p1, p2);

        GameObject card = null;
        GameObject placeToDrop = null;

        //Wenn players[0] aktiv ist
        if (p1.Data.IsActivePLayer)
        {
            //Fülle sein Mana auf
            RefillMana(p1);
            //Ziehe eine Karte für ihn
            card = p1.DrawCard();
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
            card = p2.DrawCard();
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

    void CheckWinCondition(GameObject[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log("Player " + i + ": " + players[i].GetComponent<PlayerDataController>().Data.CurrentHealth);

            if (players[i].GetComponent<PlayerDataController>().Data.CurrentHealth <= 0)
            {
                int winHelper;
                if (i == 0) winHelper = 1;
                else winHelper = 0;

                //Infotext "Game Over..." anzeigen
                info.SetActive(true);
                StartCoroutine(info.GetComponent<InfoTextController>().ShowInfoText(("GAME OVER!" + "\nPlayer " + winHelper + " has won"), 10));

                GameboardDataController.GameState = GameboardDataController.GameStatus.ending;
            }
        }
    }

    internal void ShowInfoTurnText(PlayerDataController p1, PlayerDataController p2)
    {
        string infoTurn = "Enemy Turn!";
        if (p1.isLocalPlayer && p1.Data.IsActivePLayer)
            infoTurn = "Your Turn!";
        if (p2.isLocalPlayer && p2.Data.IsActivePLayer)
            infoTurn = "Your Turn!";

        info.SetActive(true);
        StartCoroutine(info.GetComponent<InfoTextController>().ShowInfoText(infoTurn, 1));
    }
}