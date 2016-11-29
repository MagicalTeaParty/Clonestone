using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    GameboardGameplayController board;
    Button endTurnButton;

    void Start()
    {
        board = GameObject.Find("/Board").GetComponent<GameboardGameplayController>();
        endTurnButton = GameObject.Find("/Board/EndTurn/EndTurnButton").GetComponent<Button>();
    }

    void Update()
    {
        if (GameboardInitController.DetermineIfGameIsRunning() == false)
            return;

        //1. Hole die Spieler
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //2. Hole den lokalen Spieler
        if (players[0].GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            //3. hole den Zustand IsActive und gibs dem Button
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