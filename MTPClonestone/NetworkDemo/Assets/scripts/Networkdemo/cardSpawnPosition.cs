using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class cardSpawnPosition : NetworkBehaviour
{
    void Start()
    {
        if (netId.Value == 1)
        {
            this.gameObject.transform.GetChild(1).position = this.transform.position + new Vector3(0f, 1f, 1f);
            if (isLocalPlayer)
            {
                this.gameObject.transform.GetChild(1).RotateAround(this.gameObject.transform.GetChild(1).position, transform.right, -90f);
            }
            else
            {
                this.gameObject.transform.GetChild(1).RotateAround(this.gameObject.transform.GetChild(1).position, transform.right, 90f);
            }
        }

        if (netId.Value == 2)
        {
            this.gameObject.transform.GetChild(1).position = this.transform.position + new Vector3(0f, 1f, -1f);
            if (isLocalPlayer)
            {
                this.gameObject.transform.GetChild(1).RotateAround(this.gameObject.transform.GetChild(1).position, transform.right, -90f);
            }
        }
    }
}
