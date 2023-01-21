using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageScene : MonoBehaviour
{
    public static ManageScene instance = null;
    [SerializeField] GameObject finishUI;

    void Awake()
    {
        instance = this;
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.LoadScene("Core", LoadSceneMode.Additive);
    }

    public int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetCurrentLevelID()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        return int.Parse(currentLevelName.Replace("Level ", ""));
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(GetCurrentLevelIndex());
    }

    public void PlayNextLevel()
    {
        SceneManager.LoadScene(GetCurrentLevelIndex() + 1);
    }

    public void ShowFinishUI()
    {
        finishUI.SetActive(true);
    }
}
