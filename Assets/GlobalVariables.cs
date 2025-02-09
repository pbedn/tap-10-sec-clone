﻿
using System;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    protected GlobalVariables() {}

    private static GlobalVariables _instance;
    
    public int currentDollarAmount;
    public int currentUserTouchCount;

    public bool soundActive;

    public static GlobalVariables Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ScripTemplate is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        Debug.Log("Awoke GlobalVariables Instance: " + gameObject.GetInstanceID());
        
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        soundActive = true;
    }
}
