using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
