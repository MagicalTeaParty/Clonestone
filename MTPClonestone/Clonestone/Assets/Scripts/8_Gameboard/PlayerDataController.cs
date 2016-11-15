using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerDataController : NetworkBehaviour
{
    public struct PlayerData
    {
        public bool IsActive;
        //public string LoginName;
        public string GamerTag;
        //public int HandSize;
    }

    //Liste aller Karten des Spielers unabhängig von deren Aufenthaltsort
    public List<GameObject> CardList;

    public const int MaxHandSize = 10;
    private int startingHandSize;
    private bool isFirstPlayer;

    [SyncVar]
    public PlayerData Data;

    public void PlayerChange()
    {
        Data.IsActive = !Data.IsActive;
    }
}
