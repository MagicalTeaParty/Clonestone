using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Mute : MonoBehaviour {

    //bool isMute;

    [SerializeField]
    public Toggle muteToggle;
    bool value;

    public void soundMute()
    {
        //
        AudioListener.volume = value ? 1 : 0;
    }

    void Awake()
    {
        value = Convert.ToBoolean(PlayerPrefs.GetString("muteValue"));

        muteToggle = GetComponent<Toggle>();

        muteToggle.isOn = value;


        soundMute();


    }



    public void onValueChange()
    {
        value = Convert.ToBoolean(muteToggle.isOn.ToString());

        PlayerPrefs.SetString("muteValue", value.ToString());

        soundMute();
    }
}
