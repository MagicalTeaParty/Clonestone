using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;

public class InfoTextController : MonoBehaviour
{

    GameObject info;
    Text infoText;
    
    // Use this for initialization
	void Start ()
    {
        info = GameObject.Find("/Board/Info");
        infoText = GameObject.Find("/Board/Info/InfoText").GetComponent<Text>();
        info.SetActive(false);
	}
    
    internal IEnumerator ShowInfoText(string txt, float delay)
    {
        infoText.text = txt;
        yield return new WaitForSecondsRealtime(delay);
        info.SetActive(false);
    }
}