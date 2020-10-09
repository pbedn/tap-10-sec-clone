using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private static StartManager _instance;

    public static StartManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("StartManager is NULL");
            return _instance;
        }
    }

    public void StartScenePlayButton()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void HowToPlayButton()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}