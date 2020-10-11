using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private void Start()
    {
        ScenesManager.PreviousScene = SceneManager.GetActiveScene().buildIndex;
    }
}