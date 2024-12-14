using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void GOTOSETTINGSMENU()
    {
        SceneManager.LoadScene("SettingsMenu");

    }

    public void GOTOMainSMENU()
    {
        SceneManager.LoadScene("Main Menu");

    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
