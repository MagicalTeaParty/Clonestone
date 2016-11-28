using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    public GameObject player;
    public GameboardGameplayController Board;
    public Button EndTurnButton;

	// Use this for initialization
	public void ExecuteEndTurn()
    {
        if (player.GetComponent<PlayerDataController>().Data.IsActivePLayer)
        {
            Button btn = EndTurnButton.GetComponent<Button>();
            btn.onClick.AddListener(Board.EndTurn);
        }
	}
}