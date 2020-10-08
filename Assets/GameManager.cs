using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchLocation
{
    public int touchId;
    public GameObject circle;

    public TouchLocation(int newTouchId, GameObject newCircle)
    {
        touchId = newTouchId;
        circle = newCircle;
    }
}

public class GameManager : MonoBehaviour
{
    public int totalCoins = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI gameScore;
    public Button restartButton;
    public GameObject circle;

    [Header("User Actions")] [SerializeField]
    private int userTouchCount = 0;
    private List<TouchLocation> touches = new List<TouchLocation>();
    
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is NULL");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    private void Start()
    {
        restartButton.gameObject.SetActive(false);
        gameScore.gameObject.SetActive(false);
        StartCoroutine(UpdateCounter());
    }

    private void Update()
    {

    #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            print("Mouse click: " + userTouchCount);
            userTouchCount++;
            CreateCircleMouse(Input.mousePosition); // TODO: Does not work
        }
    #endif

    #if UNITY_ANDROID
        
        // One way to do it. I am counting only single finger touches here, and it is bit expensive
        // Touch is not like MouseButonDown, it is more like MouseButton => you keep pressing and counter increases
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                userTouchCount++;
                print("Touch: " + userTouchCount);
                
                // TODO: Does not work. Does not show any images ???
                touches.Add(new TouchLocation(t.fingerId, CreateCircle(t)));
            }
            else if (t.phase == TouchPhase.Ended)
            {
                var thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);
                Destroy(thisTouch.circle);
            }
        }
    #endif

    }

    GameObject CreateCircleMouse(Vector3 m)
    {
        GameObject c = Instantiate(circle);
        c.name = "Touch" + m;
        c.transform.position = m;
        print("Instiantiate object at position" + m);
        return c;
    }
    GameObject CreateCircle(Touch t)
    {
        GameObject c = Instantiate(circle);
        c.name = "Touch" + t.fingerId;
        c.transform.position = t.position;
        print("Instiantiate object at position" + t.position);
        return c;
    }

    private IEnumerator UpdateCounter()
    {
        var pressed = false;
        while (!pressed)
        {
            if (Input.GetMouseButtonDown(0)) pressed = true;

            yield return null;
        }

        var second = 10;
        while (second >= 0)
        {
            countText.text = second.ToString();
            yield return new WaitForSecondsRealtime(1);
            second--;
        }

        /* END GAME */
        restartButton.gameObject.SetActive(true);
        gameScore.gameObject.SetActive(true);
        gameScore.text = "Score: " + userTouchCount.ToString();
    }

    public int NumberOfCoins()
    {
        return totalCoins;
    }

    public void UpdateCoinTotal(int coins)
    {
        print("Giving the player " + coins + " coins");
        totalCoins += coins;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        // It should automatically exit the app when the back button is pressed on Android (put it into AWAKE).
        // Input.backButtonLeavesApp = true;
        // If that does not work then kill the process:
        // System.Diagnostics.Process.GetCurrentProcess().Kill();

        #if UNITY_EDITOR
            Debug.Log("Unity editor quit");
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
            Debug.Log("Application Quit");
        #endif    
    }
}