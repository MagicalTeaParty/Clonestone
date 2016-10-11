using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class playerNetworkController : NetworkBehaviour
{
    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

    	
	// Update is called once per frame
	void Update () {

        //Wenn ich NICHT der lokale Spieler bin, dann mach nix
        if(!isLocalPlayer)
            return;

        //(Wenn ich der lokale Spieler bin UND) Wenn die Space-Taste gedrückt wurde, dann
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Aufruf der Command-Methode damit der Server eine neue Karte erstellt
            CmdCardSpawnServer();
        }


    }

    //Alle serverseitigen Methoden benötigen das Command-Attribut
    //Gerade die Servervariablen senden nur ihre Änderungen an die Clients weiter falls dies in einer Servermethode geschieht.
    [Command]
    void CmdCardSpawnServer()
    {
        //Mittels Instantiate kann man ein neues GameObject erstellen, in diesem Fall wird das CardPrefab als Vorlage für das GameObject verwendet und an der Position und Ausrichtung von CardSpawnPosition erstellt.        
        GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.position, CardSpawnPosition.rotation);

        //Mittels NetworkServer.SpawnWithClientAuthority kann man ein GameObject - in diesem Fall die Karte (card) - über das Netzwerk bekannt machen und auch einen Besitzer festlegen.
        //connectionToClient besitzt die Daten von dem aktuellen Spieler der die Karte erzeugt hat, somit "gehört" (isAuthority) die Karte dem aktuellen Spieler der diese Methode aufgerufen hat
        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);

        //Mittels isSever kann man Abfragen ob das Objekt dem Server gehört, wenn ja kann man dem entsprechend reagieren
        //if(isServer)
        //    ruf z.B. eine Methode auf die Serverelemente steuert oder dem Client Befehle zukommen lässt
    }
}
