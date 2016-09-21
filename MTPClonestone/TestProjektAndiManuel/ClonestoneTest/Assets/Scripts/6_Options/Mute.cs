using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour {

    bool isMute;

    public void soundMute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }
}
