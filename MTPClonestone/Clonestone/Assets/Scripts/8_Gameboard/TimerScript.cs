using System;
using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    /// <summary>
    /// Folgende Variablen sind "internal", damit im GameplayController auf sie zugegriffen werden kann
    /// ...da sie nicht public sind, können sie aber nicht im Unity-GUI verändert werden.
    /// </summary>
    internal const int time4Round = 1;
    internal int TimeLeft = time4Round;

    GameboardGameplayController board;
    Text countDownText;

    /// <summary>
    /// Wird für den allerersten Zug benötigt
    /// </summary>
    bool isFirstCountDown;

    void Start()
    {
        board = GameObject.Find("/Board").GetComponent<GameboardGameplayController>();
        countDownText = GameObject.Find("/Board/EndTurn/CountDownText").GetComponent<Text>();

        countDownText.enabled = false;

        isFirstCountDown = true;
    }
    
    void Update()
    {
        if (!GameboardInitController.DetermineIfGameIsRunning())
            return;

        //Der Countdown soll noch nicht angezeigt werden
        countDownText.enabled = false;

        //Beim allerersten Zug wird der Countdown hier gestartet
        if (isFirstCountDown)
        {
            StartCoroutine("CountDown");
            isFirstCountDown = false;
        }
        
        if (TimeLeft <= 10)
        {
            //Jetzt wird der Countdown angezeigt
            countDownText.enabled = true;
            countDownText.text = (TimeLeft).ToString();

            //Wenn die Zeit abgelaufen ist, wird der Zug beendet
            if (TimeLeft < 1)
                board.EndTurn();
        }
    }

    /// <summary>
    /// Eine Endlosschleife, die nur "TimeLeft" jede Sekunde um 1 verringert
    /// Wird als Coroutine gestartet, bzw. beendet
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeLeft--;
        }
    }
}