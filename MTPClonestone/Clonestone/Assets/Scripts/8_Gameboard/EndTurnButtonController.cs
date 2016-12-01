using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    GameboardGameplayController board;
    Button endTurnButton;
    GameObject[] players;

    void Start()
    {
        board = GameObject.Find("/Board").GetComponent<GameboardGameplayController>();
        endTurnButton = GameObject.Find("/Board/EndTurn/EndTurnButton").GetComponent<Button>();
    }

    void Update()
    {
        if (!GameboardInitController.DetermineIfGameIsRunning())
            return;

        //1. Hole die Spieler
        players = GameboardInitController.Players;
        //2. Hole den lokalen Spieler
        if (players[0].GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            //3. hole den Zustand IsActive und gib's dem Button
            endTurnButton.GetComponent<Button>().enabled = players[0].GetComponent<PlayerDataController>().Data.IsActivePLayer;
        }
        else
        {
            endTurnButton.GetComponent<Button>().enabled = players[1].GetComponent<PlayerDataController>().Data.IsActivePLayer;
        }
    }

    public void ExecuteEndTurnButton()
    {
        board.EndTurn();
    }
}