using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerNetworkController : NetworkBehaviour
{
    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

    [SyncVar]
    float SyncYRot;

    void Start()
    {
        if(!isServer)
        {
            CmdRotationSync(180);   
        }
        else
        {
            Camera.main.transform.rotation = Quaternion.Euler(90, 180, 0);
        }
    }
    	
	void Update () {

        RemoteRotationSync();

        if(!isLocalPlayer)
            return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdCardSpawnServer();
        }
    }

    void RemoteRotationSync()
    {
        #region Allgemeine Änderung für ALLE Karten laut Sync-Variablen

        this.transform.rotation = Quaternion.Euler(0, SyncYRot, 0);

        #endregion
    }

    [Command]
    void CmdRotationSync(float rotationX)
    {
        SyncYRot = rotationX;
    }

    [Command]
    void CmdCardSpawnServer()
    {   
        //GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.position, CardSpawnPosition.rotation);

        GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.transform);

        card.transform.position = CardSpawnPosition.position;
        card.transform.rotation = CardSpawnPosition.rotation;

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
