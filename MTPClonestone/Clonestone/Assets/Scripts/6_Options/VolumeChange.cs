using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour {

    public Slider soundSlider;
    public AudioSource volumeAudio;
       

	public void OnValueChange()
    {
        volumeAudio.volume = soundSlider.value;

        Debug.Log(soundSlider.value);
    }


}
