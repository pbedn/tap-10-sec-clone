using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScripTemplate : MonoBehaviour
{
    #region Singleton

    private static ScripTemplate _instance;

    public static ScripTemplate Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ScripTemplate is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
