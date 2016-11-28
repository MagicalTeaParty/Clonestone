using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    public GameboardGameplayController Board;
    public Button EndTurnButton;

	// Use this for initialization
	public void ExecuteEndTurn()
    {
        Button btn = EndTurnButton.GetComponent<Button>();
        btn.onClick.AddListener(Board.EndTurn);
	}
}