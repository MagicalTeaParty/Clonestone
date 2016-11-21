using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int timeLeft = 90;
    public Text countDownText;
    public Button Endbutton;


    void Start()
    {
        StartCoroutine("CountDown");
    }

    /// <summary>
    /// Führt den Countdown aus. Soll wenn timeLeft kleiner ist als
    /// 10 Sekunden, den Text erscheinen lassen und den Countdown mit
    /// 0 beenden
    /// </summary>
    void Update()
    {
        //TODO: benötigt noch die Anbindung an GameINITController
        //if (GameboardDataController.IsRunningGame == true)
        //{


        countDownText.enabled = true;
        if (timeLeft <= 10)
        {
            countDownText.text = (timeLeft).ToString();

            if (timeLeft <= 0)
            {
                countDownText.enabled = false;
                //TODO: Spielerwechsel initialisieren,Button aktivieren für Spielerwechsel (isActivPlayer??)
                StopCoroutine("CountDown");
            }
        }
        //}
    }


    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}

