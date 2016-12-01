using UnityEngine;
using UnityEngine.Networking;

public class GameboardInitController : MonoBehaviour
{

    //Fields

    float native_width  = 1280;
    float native_height = 1024;

    public static GameObject[] Players;

    bool isInitComplete = false;

    //Methods

    void OnGUI()
    {
        //set up scaling
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new Vector3(rx, ry, 1));
    }

    void Update()
    {
        if (DetermineIfGameIsRunning() && isInitComplete == false)
        {
            //Spielstatus wird auf "running" gesetzt
            GameboardDataController.GameState = GameboardDataController.GameStatus.running;
            //Wenn das Spiel läuft wird der NetworkManagerHUD unsichtbar
            FindObjectOfType<NetworkManagerHUD>().showGUI = false;
            isInitComplete = true;
        }
    }

    /// <summary>
    /// Bestimmt, ob das Spiel läuft
    /// Es müssen beide Spieler als "IsReadyPlayer" markiert sein
    /// </summary>
    /// <returns></returns>
    public static bool DetermineIfGameIsRunning()
    {
        if(Players == null)
            Players = GameObject.FindGameObjectsWithTag("Player");

        if (Players == null || Players.Length == 0)
            return false;

        bool ok = true;

        foreach(GameObject p in Players)
        {
            ok = ok && p.GetComponent<PlayerDataController>().Data.IsReadyPlayer;
        }

        return ok;
    }

    /// <summary>
    /// Bestimmt, ob das Spiel bereit ist
    /// </summary>
    /// <returns></returns>
    public static bool DetermineIfGameIsReady()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        ///TODO Achtung! Regeln noch bearbeiten, wann ein Spiel wirklich läuft

        if (Players.Length > 1)
            return true;
        else return false;
    }
}