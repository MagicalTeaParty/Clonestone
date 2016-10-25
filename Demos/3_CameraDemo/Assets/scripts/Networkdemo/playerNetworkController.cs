using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerNetworkController : NetworkBehaviour
{
    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

    void Start()
    {
        if(!isServer)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    	
	void Update () {

        if(!isLocalPlayer)
            return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdCardSpawnServer();
        }
    }

    [Command]
    void CmdCardSpawnServer()
    {   
        GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.position, CardSpawnPosition.rotation);
                
        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);

        //Hole das Skript der neuen Karte
        cardNetworkController cnc = card.GetComponent<cardNetworkController>();

        //Setze die Rotation der Karte auf der X-Achse, da das Prefab falsch gedreht ist
        cnc.syncRotX = 90;

        if(this.netId.Value % 2 == 1)
        {
            //Ausrichtung der Karte für den Spieler auf Y-Achse
            cnc.syncRotY = 180;
        }
        
    }
}
