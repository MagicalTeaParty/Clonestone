using UnityEngine;

public class GameboardDataController : MonoBehaviour
{
    public enum GameStatus { initializing, running, ending }

    //Fields

    /// <summary>
    /// Das aktuelle Spiel läuft oder nicht.
    /// </summary>
    public static bool IsRunningGame = false;

    static System.Random rnd = new System.Random();

    //public GameObject EndTurnButton;
    //public GameObject ExitButton;
    //public GameObject OptionButton;
    //public GameObject Player1Deck;
    //public GameObject Player2Deck;
    //public GameObject Player1Dropzone;
    //public GameObject Player2Dropzone;
    //public GameObject Player1Mana;
    //public GameObject Player2Mana;
    //public GameObject Player1Hand;
    //public GameObject Player2Hand;
    //public GameObject Player1Hero;
    //public GameObject Player2Hero;
    //public GameObject Player1Life;
    //public GameObject Player2Life;

    /// <summary>
    /// Wird benutzt, um die Spielerreihenfolge zu bestimmen.
    /// Darf nur einmal aufgerufen werden --> nicht pro Spieler!!!
    /// </summary>
    /// <returns></returns>
    public static bool TossCoin()
    {
        if (rnd.Next(0, 2) == 0)
            return false;
        else return true;
    }
}