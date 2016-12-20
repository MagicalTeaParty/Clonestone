using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameboardInitController : MonoBehaviour
{

    //Fields

    float native_width  = 1280;
    float native_height = 1024;

    public static GameObject[] Players;

    bool isInitComplete = false;

    //Methods

    void Start()
    {
        Texture2D tex = PlayerDataController.LoadPNG(Application.dataPath + @"/Images/Gameboard/board.png");
        GameObject.Find("Board").GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(0.5f,0.5f));

        //tex = PlayerDataController.LoadPNG(Application.dataPath + @"/Images/Gameboard/sun.png");
        //GameObject.Find("Board/EndTurn/Sun").GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        tex = PlayerDataController.LoadPNG(Application.dataPath + @"/Images/Gameboard/Auge.png");
        GameObject.Find("Board/EndTurn/Eye").GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        tex = PlayerDataController.LoadPNG(Application.dataPath + @"/Images/GUI Elements/shield.png");
        GameObject.Find("Board/EndTurn/EndTurnButton").GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

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

    ///// <summary>
    ///// Setzt die Variable "isFirstPlayer" für beide Spieler
    ///// </summary>
    ///// <param name="p1">Spieler 1</param>
    ///// <param name="p2">Spieler 2</param>
    //public static void SetPlayerOrder(PlayerDataController p1, PlayerDataController p2)
    //{
    //    //p1.isFirstPlayer = GameboardDataController.TossCoin();
    //    p1.isFirstPlayer = true;
    //    p2.isFirstPlayer = !p1.isFirstPlayer;
    //}

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