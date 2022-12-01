using System;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        bool value = Convert.ToBoolean(PlayerPrefs.GetInt("IsFullScreen", 0));

    }

    public void SetFullScreen(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("IsFullScreen", Convert.ToInt32(value));
    }
}
