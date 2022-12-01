using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private string ExposedFieldName;
    private void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(ExposedFieldName + "Volume Value", 1);
    }

    public void SetVolume(float value)

    {
        PlayerPrefs.SetFloat(ExposedFieldName + "Volume Value", value);
        Mixer.SetFloat(ExposedFieldName, value);
    }
}
