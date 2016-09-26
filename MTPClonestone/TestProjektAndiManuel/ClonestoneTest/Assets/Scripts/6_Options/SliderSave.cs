using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderSave : MonoBehaviour {

    public Slider slider;

    void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("SliderValue",0.5f);
    }

    public void OnSliderChange(float newValue)
    {
        PlayerPrefs.SetFloat("SliderValue", newValue);
        //PlayerPrefs.Save();
    }
}
