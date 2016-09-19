using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

class cardPlayer : NetworkBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            transform.RotateAround(transform.position, transform.up, 180f);
        }
    }
}
