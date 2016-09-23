using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour,IUnivTimerListener {

    /// <summary>
    /// Hier wird Das Interface IUnivTimerListener implementiert
    /// </summary>
    public void OnTimeRanOut()
    {
        Debug.Log("Time ran out");
    }
}
