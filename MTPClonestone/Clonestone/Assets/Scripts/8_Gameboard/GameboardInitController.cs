using UnityEngine;
using System.Collections;

public class GameboardInitController : MonoBehaviour {

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
        if (DetermineIfGameIsReady())
        {
            //Sobald beide Spieler dem Spiel beigetreten sind,

            ///TODO Hier gehört noch was erledigt
            
            GameboardDataController.IsRunningGame = true;
        }
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