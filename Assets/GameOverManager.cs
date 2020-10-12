using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI lastGameScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI totalDollarAmountText;

    private int _bestScore;
    private int _totalDollarAmount;

    private void Start()
    {
        _bestScore = PlayerPrefs.GetInt("best_score");
        _totalDollarAmount = PlayerPrefs.GetInt("total_dollar_amount");
        
        print(">>>>>>>>>>>>>>>>");
        Debug.Log(_bestScore);
        Debug.Log(_totalDollarAmount);
        print(">>>>>>>>>>>>>>>>");
        
        ScenesManager.PreviousScene = SceneManager.GetActiveScene().buildIndex;
        lastGameScoreText.text = "Your Score: " + GlobalVariables.Instance.currentUserTouchCount;
        bestScoreText.text = GetBestScore();
        totalDollarAmountText.text = GetTotalDollarAmount();
        
        PlayerPrefs.SetInt("best_score", _bestScore);
        PlayerPrefs.SetInt("total_dollar_amount", _totalDollarAmount);
    }

    private string GetTotalDollarAmount()
    {
        _totalDollarAmount += GlobalVariables.Instance.currentDollarAmount;
        return _totalDollarAmount.ToString();
    }

    private string GetBestScore()
    {
        var currentScore = GlobalVariables.Instance.currentUserTouchCount;
        if (currentScore > _bestScore)
        {
            _bestScore = currentScore;
        }

        return "Best: " + _bestScore;
    }
}
