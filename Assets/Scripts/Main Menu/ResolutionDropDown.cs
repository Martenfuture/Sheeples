using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropDown : MonoBehaviour
{
    Resolution[] resolutions;

    private void Awake()
    {
        bool IsOpenForTheFirstTime = Convert.ToBoolean(PlayerPrefs.GetInt("GameOpenedForFirstTime", 1));
        int currentresolution = PlayerPrefs.GetInt("CurrentResolution");
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width}) X {resolutions[i].height }");
            if(IsOpenForTheFirstTime && resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                IsOpenForTheFirstTime = false;
                PlayerPrefs.SetInt("GameOpenedForFirstTime", Convert.ToInt32(IsOpenForTheFirstTime));
                currentresolution = i;
                PlayerPrefs.SetInt("Currentresolution", i);
            }
            TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdown.value = currentresolution;
            dropdown.RefreshShownValue();
        }
    }
    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex];
        FindObjectOfType<CanvasScaler>().referenceResolution = new Vector2(resolution.width, resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
}
