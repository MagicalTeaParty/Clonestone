using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class cameraController : NetworkBehaviour
{
    void Start()
    {
        if (netId.Value == 2 && isLocalPlayer)
        {
            Camera.main.transform.RotateAround(Camera.main.transform.position, transform.up, 180f);
            GameObject.Find("Gameboard").transform.RotateAround(GameObject.Find("Gameboard").transform.position, transform.up, 180f);
        }
    }

    //public Camera PlayerCamera;
    //
    //void Start()
    //{
    //    if (!isLocalPlayer)
    //    {
    //        PlayerCamera.enabled = false;
    //    }

    //    if (netId.Value == 1)
    //    {
    //        PlayerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+2f, -5f);
    //        PlayerCamera.transform.RotateAround(this.transform.position, transform.forward, 180f);
    //    }
    //    else
    //    {
    //        PlayerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+2f, -5f);
    //        //PlayerCamera.transform.RotateAround(this.transform.position, transform.up, 90f);
    //    }
    //}
}
