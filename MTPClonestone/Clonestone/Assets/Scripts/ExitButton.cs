using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour {

	public void ExitClient()
    {
        //Beendet den Client
        Application.Quit();

        //Dient nur zur Überprüfung da Application.Quit() nicht im "Testrun" funktioniert
        Debug.Log("EXIT");
    }

    public void LogOut()
    {
        SceneManager.LoadScene(3);
    }
}
