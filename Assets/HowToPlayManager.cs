using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    #region Singleton

    private static HowToPlayManager _instance;

    public static HowToPlayManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("HowToPlayManager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    public void ReturnToStartScene()
    {
        SceneManager.LoadScene("Start");
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
