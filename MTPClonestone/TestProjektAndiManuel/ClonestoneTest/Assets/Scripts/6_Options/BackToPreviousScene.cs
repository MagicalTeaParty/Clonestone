using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToPreviousScene : MonoBehaviour {

	public void Back()
    {
        int previousLevel = PlayerPrefs.GetInt("previousLevel");
        SceneManager.LoadScene(previousLevel);

    }
}
