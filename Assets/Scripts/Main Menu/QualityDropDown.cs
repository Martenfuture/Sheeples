using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualityDropDown : MonoBehaviour
{
    private void Awake()
    {
        TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();
        List<string> options = new List<string>();
        dropdown.ClearOptions();
        foreach (var qualityoption in QualitySettings.names)
        {
            options.Add(qualityoption);

        }

        dropdown.AddOptions(options);
        dropdown.value = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
        dropdown.RefreshShownValue();

    
    
    }
    public void SetQuality (int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        PlayerPrefs.SetInt("QualityLevel", QualityIndex);
    }
}
