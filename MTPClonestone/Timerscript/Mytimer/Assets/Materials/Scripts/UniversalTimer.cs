using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniversalTimer : MonoBehaviour {

    //Dieser Gernerische Timer kann überall eingesetzt werden
    // na klar er ist generisch-- universell einsetzbar ;-)

    public enum TimerType {CountDown,Rope};

    public TimerType timerType = TimerType.CountDown;



    public List<GameObject> listeners = new List<GameObject>();
    public List<GameObject> subscribers = new List<GameObject>();

    
    public GUIText text; 
    public float TimeInSeconds = 5f;
    
 
    void Start()
    {
        foreach (GameObject subcribers in subscribers)
        {
            IUnivTimerSubscriber component = (IUnivTimerSubscriber)subcribers.GetComponent(typeof(IUnivTimerSubscriber));
            if (component != null)
            {
                component.Initialize(this);

            }
        }
        StartCoroutine(Countdown());
    }


    /// <summary>
    /// Der IEnumerator Zählt die Sekunden herunter
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown()
    {
        while (TimeInSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            TimeInSeconds--;
            UpdateText(TimeInSeconds);
        }
        foreach (GameObject listener in listeners)
        {
            IUnivTimerListener component = (IUnivTimerListener)listener.GetComponent(typeof(IUnivTimerListener));
            if (component != null)
            {
                component.OnTimeRanOut();

            }
        }
    }

    /// <summary>
    /// Schreibt die jeweiligen Ziffern als Text auf
    /// </summary>
    void UpdateText(float amount)
    {
        text.text = Mathf.Max(0, amount).ToString();
    }


    /// <summary>
    /// Adds time to a Countdown
    /// </summary>
    /// <param name="Amount"></param>
    public void AddTime(float Amount)
    {
        TimeInSeconds += Amount;
        UpdateText(TimeInSeconds);
    }

}
