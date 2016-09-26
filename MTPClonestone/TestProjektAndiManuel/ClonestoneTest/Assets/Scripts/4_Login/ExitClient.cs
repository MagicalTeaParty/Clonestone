using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitClient : MonoBehaviour {

	public void onClick()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
}
