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
        _bestScore = GlobalVariables.Instance.bestScore;
        ScenesManager.PreviousScene = SceneManager.GetActiveScene().buildIndex;
        lastGameScoreText.text = "Your Score: " + GlobalVariables.Instance.currentUserTouchCount;
        bestScoreText.text = GetBestScore();
        totalDollarAmountText.text = GetTotalDollarAmount();
    }

    private string GetTotalDollarAmount()
    {
        GlobalVariables.Instance.totalDollarAmount += GlobalVariables.Instance.currentDollarAmount;
        return GlobalVariables.Instance.totalDollarAmount.ToString();
    }

    private string GetBestScore()
    {
        var currentScore = GlobalVariables.Instance.currentUserTouchCount;
        if (currentScore > _bestScore)
        {
            _bestScore = currentScore;
            GlobalVariables.Instance.bestScore = currentScore;
        }

        return "Best: " + _bestScore;
    }
}
