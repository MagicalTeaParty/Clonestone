using UnityEngine;
using System.Collections.Generic;

public class playerBoardScript : MonoBehaviour
{
    public List<GameObject> CardsOnBoard;

    void OnTriggerEnter(Collider enter)
    {
        if (!CardsOnBoard.Contains(enter.gameObject))
        {
            CardsOnBoard.Add(enter.gameObject);
        }
    }

    void OnTriggerExit(Collider exit)
    {
        if (CardsOnBoard.Contains(exit.gameObject))
        {
            CardsOnBoard.Remove(exit.gameObject);
        }
    }
}
