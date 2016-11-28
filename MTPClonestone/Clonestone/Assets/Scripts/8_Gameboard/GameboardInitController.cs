using UnityEngine;
using UnityEngine.Networking;

public class GameboardInitController : MonoBehaviour
{

    //Fields

    float native_width  = 1280;
    float native_height = 1024;

    public static GameObject[] Players;

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
        if (GameboardInitController.DetermineIfGameIsRunning())
        {
            GameboardDataController.GameState = GameboardDataController.GameStatus.running;

            FindObjectOfType<NetworkManagerHUD>().showGUI = false;
        }
    }

    public static bool DetermineIfGameIsRunning()
    {
        if(Players == null)
            Players = GameObject.FindGameObjectsWithTag("Player");

        if (Players == null || Players.Length == 0)
            return false;

        bool ok = true;

        foreach(var p in Players)
        {
            ok = ok && p.GetComponent<PlayerDataController>().Data.IsReadyPlayer;
        }

        return ok;
    }

    public static bool DetermineIfGameIsReady()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        ///TODO Achtung! Regeln noch bearbeiten, wann ein Spiel wirklich läuft

        if (Players.Length > 1)
            return true;
        else return false;
    }
}