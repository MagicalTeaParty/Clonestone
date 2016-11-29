using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    internal const int time4Round = 1;
    internal int TimeLeft = time4Round;

    GameboardGameplayController board;
    Text countDownText;

    bool isFirstCountDown;

    void Start()
    {
        countDownText = GameObject.Find("/Board/EndTurn/CountDownText").GetComponent<Text>();
        board = GameObject.Find("/Board").GetComponent<GameboardGameplayController>();

        countDownText.enabled = false;

        isFirstCountDown = true;
    }

    /// <summary>
    /// Führt den Countdown aus. Soll wenn timeLeft kleiner ist als
    /// 10 Sekunden, den Text erscheinen lassen und den Countdown mit
    /// 0 beenden
    /// </summary>
    void Update()
    {
        if (!GameboardInitController.DetermineIfGameIsRunning())
            return;

        if (isFirstCountDown)
        {
            StartCoroutine("CountDown");
            isFirstCountDown = false;
        }

        countDownText.enabled = false;
        
        if (TimeLeft <= 10)
        {
            countDownText.enabled = true;
            countDownText.text = (TimeLeft).ToString();

            if (TimeLeft < 1)
            {
                //Wenn die Zeit abgelaufen ist, muss "CountDown" beendet werden.
                board.EndTurn();
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