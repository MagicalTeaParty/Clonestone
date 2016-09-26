using UnityEngine;
using System.Collections;

public class ExitClient : MonoBehaviour {

	public void onClick()
    {
        //Beendet den Client
        Application.Quit();

        //Dient nur zur Überprüfung da Application.Quit() nicht im "Testrun" funktioniert
        Debug.Log("EXIT");
    }
}
