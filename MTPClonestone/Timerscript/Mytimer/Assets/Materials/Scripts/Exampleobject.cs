using UnityEngine;
using System.Collections;
using System;

public class Exampleobject : MonoBehaviour, IUnivTimerSubscriber
{
    public void Initialize(UniversalTimer universalTimer)
    {
        _UnTimer = universalTimer;
    }
    private UniversalTimer _UnTimer;
    public float TimeToAdd = 5f;

    // Use this for initialization

    
    void OnMouseDown()
    {
        _UnTimer.AddTime(TimeToAdd);
    }
}
