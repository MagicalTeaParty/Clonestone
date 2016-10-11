﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.04f)]
public class cardNetworkController : NetworkBehaviour
{   
    [SyncVar]
    private Vector3 syncPos; //Position der Karte zum Synchronisieren

    [SyncVar]
    private float syncRotY; //Y-Rotation der Karte zum Synchronisieren
    
    //Update wird für jeden Frame ausgeführt - Achtung: Alle Methodenaufrufe darin werden auch in jedem Frame ausgeführt!    
    void Update()
    {
        //Hier teilt der Client dem Server alle relevanten Daten Daten mit die in jedem Frame aktualisiert werden sollen - in unserem Fall die Position
        TransmitPositionSync();

        //Hier holt der Client die Daten der SyncVariablen und passt die lokale Kopie an
        RemotePositionSync();

        //Prüfe ob die R-Taste gedrückt wurde
        if(Input.GetKeyDown(KeyCode.R))
        {
            //man kann nur die eigenen Karten drehen - da man über die fremden Karten keine Authority hat, Command-Methoden dürfen nur von GameObjects aufgerufen werden die auch die Authority beim lokalen Spieler haben.
            if(hasAuthority)
            {
                //Damit die Karte hin und her gedreht wird, wird abhängig von der aktuellen Rotation der Karte diese auf 0.9 oder 0 gestellt
                if(!(this.transform.rotation.y == 0.9f))
                    TransmitFlipSync(0.9f);
                else
                    TransmitFlipSync(0.0f);
            }
        }

    }

    /// <summary>
    /// Hier wird mittels ClientCallback die Command-Methode aufgerufen
    /// </summary>
    /// <param name="angle"></param>
    [ClientCallback]
    void TransmitFlipSync(float angle)
    {
        CmdFlipSync(angle);
    }

    /// <summary>
    /// Servermethode um die Sync-Variable für die y-Rotation festzulegen
    /// </summary>
    /// <param name="angle"></param>
    [Command]
    void CmdFlipSync(float angle)
    {
        //Festlegen der neuen Y-Rotation für die Karte
        syncRotY = angle;
    }
    
    /// <summary>
    /// Diese Methode aktualisiert die Daten des lokalen GameObjects (der Karte) mit den Werten die vom Server kommen - den Sync-Variablen
    /// </summary>
    void RemotePositionSync()
    {
        #region Allgemeine Änderung für ALLE Karten laut Sync-Variablen
        //Die Rotation wird vom Server bestimmt, deswegen außerhalb von hasAuthority und das GameObject (die Karte) wird anhand von syncRotY rotiert
        this.transform.rotation = new Quaternion(this.transform.rotation.x, syncRotY, this.transform.rotation.z, this.transform.rotation.w);
        #endregion

        #region Änderungen für alle FREMDEN Karten laut Sync-Variablen
        //Wenn es ein GameObject (Karte) ist die nicht mir gehört, dann soll diese ihre Koordinaten von der Server Sync-Variablen holen und verwenden.
        if(!hasAuthority)
        {
            //Überschreibe die Koordinaten mit denen der sync-Variable
            this.transform.position = syncPos;
        } 
        #endregion
    }

    /// <summary>
    /// Legt am Server die Koordinaten der Karte fest
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rotY"></param>
    /// <param name="rotX"></param>
    [Command]
    void CmdPositionSync(Vector3 pos)
    {
        //Werte von syncVariablen sollten in Command-Methoden gesetzt werden, da diese der Server ausführt
        syncPos = pos; //Sichern der aktuellen Position
    }

    /// <summary>
    /// Anfrage des Clients an den Server
    /// </summary>
    [ClientCallback]
    void TransmitPositionSync()
    {
        //Man sollte nur Befehle an den Server schicken für Objekte wo man die Authority hat, deswegen die Prüfung darauf
        if(hasAuthority)
        {   
            //Übermittle der Servermethode die Position der Karte
            CmdPositionSync(this.transform.position);
        }
    }

    #region lokale Steuerung der Karte, kümmert sich hier um Drag & Drop

    private Vector3 screenPoint;
    private Vector3 offset;

    /// <summary>
    /// Für die korrekte Berechnung bei Drag & Drop
    /// </summary>
    void OnMouseDown()
    {
        //Save object's position
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        //    //Save difference between the mouse' and the object's positions
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    /// <summary>
    /// Für die Bewegung der Karte
    /// </summary>
    void Move()
    {
        //Save the current mouse position
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //Convert the screen point to world point plus the difference between mouse and object
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //Set the object's position to the new position
        transform.position = curPosition;
    }

    /// <summary>
    /// Das eigentliche Drag & Drop
    /// </summary>
    void OnMouseDrag()
    {
        //nur EIGENE Karten dürfen verschoben werden
        if(hasAuthority)
        {
            Move();
        }
    } 
    #endregion

}
