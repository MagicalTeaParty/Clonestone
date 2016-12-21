using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MainMenue : MonoBehaviour {

    // Variablen die sich auf IO Objekte in der MainMenue Scene beziehen.
    public Text welcome;

    void Start () {

        //Übernahme der beiden via PlayerPrefs gespeicherten Variablen (PlayerPrefs.Get...)
        string gamertag = PlayerPrefs.GetString("Gamertag");
        int playerid = PlayerPrefs.GetInt("PlayerID");

        // TEST TEST TEST 
        //Debug.Log(gamertag);
        //Debug.Log(playerid);

        /// TODO - .text
        welcome.text = "Welcome " + gamertag; 

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
