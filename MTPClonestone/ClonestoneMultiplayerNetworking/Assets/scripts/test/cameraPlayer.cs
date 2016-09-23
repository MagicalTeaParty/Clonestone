using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class cameraPlayer : NetworkBehaviour
{
    void Start()
    {
        if (!isLocalPlayer)
        {
            transform.position = new Vector3(0.0f, 0.0f, 2.0f);
            transform.RotateAround(transform.position, transform.right, 180f);
        }
    }	
}