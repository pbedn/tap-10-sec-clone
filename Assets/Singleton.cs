using UnityEngine;

public class Singleton : MonoBehaviour
{
    #region Singleton
    
    protected Singleton() {}

    private static Singleton _instance;

    public static Singleton Instance
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
        Debug.Log("Awoke Singleton Instance: " + gameObject.GetInstanceID());
        
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion
}
