using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScriptAI : MonoBehaviour
{
    /// <summary>
    /// Folgende Variablen sind "internal", damit im GameplayController auf sie zugegriffen werden kann
    /// ...da sie nicht public sind, können sie aber nicht im Unity-GUI verändert werden.
    /// </summary>
    internal const int time4Round = 2;

    internal int TimeLeft = time4Round;
    
    void Start()
    {
    }

    void Update()
    {
        if (GameboardDataController.GameState != GameboardDataController.GameStatus.running)
            return;
        
    }
    
    /// <summary>
    /// Eine Endlosschleife, die nur "TimeLeft" jede Sekunde um 1 verringert.
    /// Das ist notwendig, um den Countdown (= TimeLeft) im Spiel anzuzeigen.
    /// Wird als Coroutine gestartet, bzw. beendet.
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            TimeLeft--;
        }
    }
}