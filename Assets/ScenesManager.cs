using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static int PreviousScene;

    // public void LoadStartScene()
    // {
        // SceneManager.LoadScene("Start");
    // }
    
    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    
    public static void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    // reload the current scene
    // public void ReloadCurrentScene()
    // {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    // load the previous scene
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(ScenesManager.PreviousScene);
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
        #endif
            
        #if UNITY_ANDROID
            Application.Quit();
            Debug.Log("Application Quit");
        #endif    
    }
}
