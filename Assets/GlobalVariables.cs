
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    protected GlobalVariables() {}

    private static GlobalVariables _instance;
    
    public int bestScore;
    public int totalDollarAmount;
    public int currentDollarAmount;
    public int currentUserTouchCount;

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
}
