using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateCoinTotal(int coins)
    {
        print("Giving the player " + coins + " coins");
        GameManager.Instance.totalCoins += coins;
    }

    public void StartScenePlayButton()
    {
        SceneManager.LoadSceneAsync(sceneBuildIndex: 1);
    }
}
