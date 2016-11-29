using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    public GameObject player;
    public GameboardGameplayController Board;
    public Button EndTurnButton;

    void Update()
    {
        this.EndTurnButton.GetComponent<Button>().enabled = player.GetComponent<PlayerDataController>().Data.IsActivePLayer;
    }

    public void ExecuteEndTurnButton()
    {
        Board.EndTurn();
    }
}