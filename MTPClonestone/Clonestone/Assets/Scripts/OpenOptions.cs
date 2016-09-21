using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenOptions : MonoBehaviour {

	public void OptionsOpen()
    {
        PlayerPrefs.SetInt("previousLevel", Application.loadedLevel);
        SceneManager.LoadScene(5);
    }
}
