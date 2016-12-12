using UnityEngine;

public class GameboardDataController : MonoBehaviour
{
    public enum GameStatus { initializing, running, ending }

    //Fields

    /// <summary>
    /// Bestimmt den aktuellen Status des Spiels
    /// Das Spiel beginnt immer mit der Initialisierung
    /// </summary>
    public static GameStatus GameState = GameStatus.initializing;
}