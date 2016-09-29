using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenOptions : MonoBehaviour {

	public void OptionsOpen()
    {
        PlayerPrefs.SetInt("previousLevel", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(5);
    }
}
