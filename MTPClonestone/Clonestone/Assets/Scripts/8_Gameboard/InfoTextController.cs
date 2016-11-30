using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;

public class InfoTextController : MonoBehaviour
{

    public GameObject Info;
    public Text InfoText;
    private float seconds = 2;
    
    // Use this for initialization
	void Start () {
	Info.SetActive(false);
	}
    /// <summary>
    /// Zeigt einen mitgegebenen InfoText für eine
    /// gewisse Zeitspanne. Eine Coroutine zählt herunter
    /// und inzwischen bleibt das Textfeld aktiv
    /// </summary>
    /// <param name="tex"></param>
    /// <param name="sec"></param>
    internal void showInfoText(string tex,float sec)
    {
        this.seconds = sec;
        StartCoroutine("WaitForAMoment");
        if (seconds>=0)
        {
         Info.SetActive(true);
         InfoText.text = tex;
        }
        StopCoroutine("WaitForAMoment");
        Info.SetActive(false);
    }
    IEnumerator WaitforAMoment()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds--;
        }
    }
}
