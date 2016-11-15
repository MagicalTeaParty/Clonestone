using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class PlayerDeckinitController : NetworkBehaviour {

    public GameObject CardPrefab; //Platzhalter für das Karten-Prefab
    public Transform CardSpawnPosition; //Platzhalter für die Position des Spawnpunkts des aktuellen Spielers

    public void onClick()
    {
        StartCoroutine("getDeck");
    }


    public IEnumerator getDeck()
    {
        //Mittels JsonUtility.FromJson kann man ein JSON-Objekt auf ein C# Objekt mappen/umwandeln.        
        //CardDataController.CardData data = JsonUtility.FromJson<CardDataController.CardData>(jsonstring);
        
        //Beispiel für ein Json-Objekt
        //{\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }

        string url = "http://localhost:53861/Deck/GetDeck";
        WWWForm form = new WWWForm();
        form.AddField("idDeck", 1);

        WWW returnJson = new WWW(url, form);
        Debug.Log("hallo");

        yield return returnJson.text;

        
        //Da JsonUtility.FromJson nur mit Json-Objekten aber nicht mit Json-Arrays umgehen kann wird zuerst gesplittet um die einzelnen Objekte zu erhalten.
        string[] jsonArray =returnJson.text.Split(new char[] { '{' ,'}' });
        string helper;

        foreach (var item in jsonArray)
        {
            //Nach dem Split gibt es auch unnötige Zeichen die wir nicht für das Json-Objekt benötigen. Json-Objekte erkenne wir daran, dass diese mit " beginnen.
            if (item[0] == '"' )
            {
                //Da nach dem Split die {} des Json-Objekts fehlen, werden diese wieder hinzugefügt
                helper = "{" + item + "}";

                //Mittels FromJson wird nun der String der auf helper steht - dieser ist ein Json-Objekt - in ein Objekt vom Typ CardData umgewandelt
                CardDataController.CardData cardData = JsonUtility.FromJson<CardDataController.CardData>(helper);

                ///TODO If Herocard, dann
                #region GameObjekt für die cardData erstellen und cardData dem Hero zuweisen

                #endregion

                ///TODO else,.... (wenn nicht hero)
                #region GameObjekt für die cardData erstellen und cardData zuweisen

                CmdCardSpawnServer(cardData);

                #endregion
            }
        }

        Debug.Log("hallo");
        
    }

    //Alle serverseitigen Methoden benötigen das Command-Attribut
    //Gerade die Servervariablen senden nur ihre Änderungen an die Clients weiter falls dies in einer Servermethode geschieht.
    [Command]
    void CmdCardSpawnServer(CardDataController.CardData cardData)
    {
        //Mittels Instantiate kann man ein neues GameObject erstellen, in diesem Fall wird das CardPrefab als Vorlage für das GameObject verwendet und an der Position und Ausrichtung von CardSpawnPosition erstellt.        
        GameObject card = (GameObject)Instantiate(CardPrefab, CardSpawnPosition.position, CardSpawnPosition.rotation);

        ///TODO Übergabeparameter cardData auf die Instanz card speichern

        //Mittels NetworkServer.SpawnWithClientAuthority kann man ein GameObject - in diesem Fall die Karte (card) - über das Netzwerk bekannt machen und auch einen Besitzer festlegen.
        //connectionToClient besitzt die Daten von dem aktuellen Spieler der die Karte erzeugt hat, somit "gehört" (isAuthority) die Karte dem aktuellen Spieler der diese Methode aufgerufen hat
        NetworkServer.SpawnWithClientAuthority(card, this.connectionToClient);
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
