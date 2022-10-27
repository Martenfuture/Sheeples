using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageScene : MonoBehaviour
{
    public static ManageScene instance = null;

    void Awake()
    {
        instance = this;
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.LoadScene("Core", LoadSceneMode.Additive);
    }
}
