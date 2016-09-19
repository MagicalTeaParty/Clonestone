using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

class cardPlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        transform.RotateAround(transform.position, transform.up, 180f);
    }
    void Start()
    {
        if (!isLocalPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}