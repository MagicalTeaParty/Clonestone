using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public int TimeLeft = 75;
    public Text CountDownText;
    public GameboardGameplayController TurnEnder;

    void Start()
    {
        if (CountDownText == null)
        {
            CountDownText = GameObject.Find("/Board/EndTurn/CountDownText").GetComponent<Text>();
        }
    }

    /// <summary>
    /// Führt den Countdown aus. Soll wenn timeLeft kleiner ist als
    /// 10 Sekunden, den Text erscheinen lassen und den Countdown mit
    /// 0 beenden
    /// </summary>
    void Update()
    {

        //Debug.Log(TimeLeft);

        if (TimeLeft <= 10)
        {
            CountDownText.enabled = true;
            CountDownText.text = (TimeLeft).ToString();

            if (TimeLeft <= 1)
            {
                CountDownText.enabled = false;

                //Wenn die Zeit abgelaufen ist, muss "CountDown" beendet werden.
                TurnEnder.EndTurn();
            }
        }
    }

    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
    }
}