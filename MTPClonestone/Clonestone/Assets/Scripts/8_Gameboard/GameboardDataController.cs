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

    //static System.Random rnd = new System.Random();

    ///// <summary>
    ///// Wird benutzt, um die Spielerreihenfolge zu bestimmen.
    ///// Darf nur einmal aufgerufen werden --> nicht pro Spieler!!!
    ///// </summary>
    ///// <returns></returns>
    //public static bool TossCoin()
    //{
    //    if (rnd.Next(0, 2) == 0)
    //        return false;
    //    else return true;
    //}
}