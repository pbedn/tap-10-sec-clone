using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int totalCoins = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI gameScore;
    public Button restartButton;
    public GameObject circle;
    public GameObject userCountText;
    public Transform pointerSpawnHolder;

    [Header("User Actions")]
    [SerializeField] private int userTouchCount = 0;
    // [SerializeField] private List<GameObject> _pointers = new List<GameObject>();
    private readonly Queue<GameObject> _pointersQ = new Queue<GameObject>();
    private readonly Queue<GameObject> _textQ = new Queue<GameObject>();
    private GameObject _lastUserPointer;
    private GameObject _lastUserCount;
    private Image _lastUserPointerImage;
    private TextMeshProUGUI _lastUserCountText;

    private bool _timerIsOn = false;
    
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
        if (Input.GetMouseButtonDown(0) && _timerIsOn)
        {
            try
            {
                StopCoroutine(FadeOut.FadeImage(_lastUserPointerImage));
                StopCoroutine(FadeOut.FadeText(_lastUserCountText));
                Destroy(_lastUserCount);
                Destroy(_lastUserPointer);
                _pointersQ.Dequeue();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            UpdateMouseClick();
        }
    #endif

    #if UNITY_ANDROID
        
        // One way to do it. I am counting only single finger touches here, and it is bit expensive
        // Touch is not like MouseButonDown, it is more like MouseButton => you keep pressing and counter increases
        if (Input.touchCount > 0 && _timerIsOn)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                try
                {
                    StopCoroutine(FadeOut.FadeImage(_lastUserPointerImage));
                    StopCoroutine(FadeOut.FadeText(_lastUserCountText));
                    Destroy(_lastUserCount);
                    Destroy(_lastUserPointer);
                    _pointersQ.Dequeue();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                UpdateTouch(t);
            }
        }
    #endif

    }

    private void UpdateTouch(Touch t)
    {
        userTouchCount++;
        _lastUserPointer = CreateCircleTouch(t);
        _lastUserCount = CreateUserCountText(t.position);
        _pointersQ.Enqueue(_lastUserPointer);
        _textQ.Enqueue(_lastUserCount);
        _lastUserPointerImage = _lastUserPointer.GetComponent<Image>();
        _lastUserCountText = _lastUserCount.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut.FadeImage(_lastUserPointerImage));
        StartCoroutine(FadeOut.FadeText(_lastUserCountText));
        }

    private void UpdateMouseClick()
    {
        userTouchCount++;
        _lastUserPointer = CreateCircleMouse(Input.mousePosition);
        _lastUserCount = CreateUserCountText(Input.mousePosition);
        _pointersQ.Enqueue(_lastUserPointer);
        _textQ.Enqueue(_lastUserCount);
        _lastUserPointerImage = _lastUserPointer.GetComponent<Image>();
        _lastUserCountText = _lastUserCount.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut.FadeImage(_lastUserPointerImage));
        StartCoroutine(FadeOut.FadeText(_lastUserCountText));
    }

    GameObject CreateCircleMouse(Vector3 m)
    {
        GameObject c = Instantiate(circle, pointerSpawnHolder.transform, true);
        c.name = "Click" + m;
        c.transform.position = m;
        c.transform.localScale = new Vector3(0.4f ,0.4f, 0.4f);
        print("Instantiate object at position" + m);
        return c;
    }
    GameObject CreateCircleTouch(Touch t)
    {
        GameObject c = Instantiate(circle, pointerSpawnHolder.transform, true);
        c.name = "Touch" + t.fingerId;
        c.transform.position = t.position;
        print("Instantiate object at position" + t.position);
        return c;
    }

    GameObject CreateUserCountText(Vector3 position)
    {
        GameObject t = Instantiate(userCountText, pointerSpawnHolder, true);
        var text = t.GetComponent<TextMeshProUGUI>();
        text.text = userTouchCount.ToString();
        t.name = "UserCountText: " + text.text;
        t.transform.position = position + new Vector3(0, 70, 0);
        t.transform.localScale = new Vector3(0.5f ,0.5f, 0.5f);
        print("Instantiate text object at position" + t.transform.position);
        return t;
    }

    private IEnumerator UpdateCounter()
    {
        var pressed = false;
        while (!pressed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pressed = true;
                _timerIsOn = true;
                
                /*START GAME*/
                UpdateMouseClick();
            }
 
            else if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    pressed = true;
                    _timerIsOn = true;
                    
                    /*START GAME*/
                    UpdateTouch(t);
                }
            }
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
        _timerIsOn = false;

        /*CLEANING*/
        foreach (var pointer in _pointersQ)
        {
            Destroy(pointer);
        }
        _pointersQ.Clear();
        
        foreach (var text in _textQ)
        {
            Destroy(text);
        }
        _textQ.Clear();
        
        restartButton.gameObject.SetActive(true);
        gameScore.gameObject.SetActive(true);
        gameScore.text = "Score: " + userTouchCount;
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
        SceneManager.LoadScene("Game");
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