using UnityEngine;
using System.Collections;

public class GameboardInitController : MonoBehaviour {

    float native_width  = 1280;
    float native_height = 1024;

    void OnGUI()
    {
        //set up scaling
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity,new Vector3(rx, ry, 1));
    }
}
