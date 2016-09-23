using UnityEngine;
using UnityEngine.Networking;

public class endTurn : NetworkBehaviour
{
    public bool isMyTurn;

    void OnMouseDown()
    {
        isMyTurn = !isMyTurn;
    }
}