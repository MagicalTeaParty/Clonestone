using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour {

    public Slider soundSlider;
    private AudioSource volumeAudio;
       

    private void Awake()
    {
        GameObject musicPlayer = GameObject.Find("MusicPlayer");
        AudioSource musicPlayerAudioSrc = musicPlayer.GetComponent<AudioSource>();
        volumeAudio = musicPlayerAudioSrc;
    }

	public void OnValueChange()
    {
        if (volumeAudio != null)
        {
            volumeAudio.volume = soundSlider.value;

            Debug.Log(soundSlider.value);
        }
    }


}
