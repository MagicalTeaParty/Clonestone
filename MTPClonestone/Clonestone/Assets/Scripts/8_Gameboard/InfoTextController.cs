using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;

public class InfoTextController : MonoBehaviour
{
    GameObject info;
    Text infoText;

	void Start ()
    {
        info = GameObject.Find("/Board/Info");
        infoText = GameObject.Find("/Board/Info/InfoText").GetComponent<Text>();
        info.SetActive(false);
	}
    
    /// <summary>
    /// Zeigt einen Infotext an.
    /// </summary>
    /// <param name="infoText">Der anzuzeigende Text</param>
    /// <param name="delay">So lange wird der Text angezeigt</param>
    /// <returns></returns>
    internal IEnumerator ShowInfoText(string infoText, float delay)
    {
        this.infoText.text = infoText;
        yield return new WaitForSecondsRealtime(delay);
        info.SetActive(false);
    }
}