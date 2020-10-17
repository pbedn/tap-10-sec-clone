using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject activeSoundButton;
    public GameObject inActiveSoundButton;


    private void Start()
    {
        ToggleButtonUI(GlobalVariables.Instance.soundActive);
        ScenesManager.PreviousScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ToggleButton()
    {
        GlobalVariables.Instance.soundActive = !GlobalVariables.Instance.soundActive;
        ToggleButtonUI(GlobalVariables.Instance.soundActive);
    }

    private void ToggleButtonUI(bool soundActive)
    {
        
        if (soundActive)
        {
            activeSoundButton.SetActive(true);
            inActiveSoundButton.SetActive(false);
        }
        else
        {
            activeSoundButton.SetActive(false);
            inActiveSoundButton.SetActive(true);
        }
    }
}