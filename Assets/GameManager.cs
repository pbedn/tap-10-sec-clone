using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The GameManager is NULL.");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        
        // It should automatically exit the app when the back button is pressed on Android.
        // Input.backButtonLeavesApp = true;
        // If that does not work then kill the process:
        // System.Diagnostics.Process.GetCurrentProcess().Kill();

    }

    public int totalCoins = 0;

    public int NumberOfCoins()
    {
        return totalCoins;
    }
    
    public void QuitApplication()
    {
        #if UNITY_EDITOR
            Debug.Log("Unity editor quit");
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
            Debug.Log("Application Quit");
        #endif
    }
}
