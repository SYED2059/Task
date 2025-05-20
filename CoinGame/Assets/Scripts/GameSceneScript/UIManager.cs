using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI timerText;

    public TextMeshProUGUI finalScoreText;

    [Header("GameObject")]
    [SerializeField] private GameObject gameOverPanel;

    public static UIManager Data;

    void Awake()
    {
        Data = this;
    }

    public void UpdateScoreFN(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateTimerFN(float time)
    {
        timerText.text = $"Time: {Mathf.Ceil(time)}";
    }

    public void ShowGameOver(bool show, int finalScore = 0)
    {
        gameOverPanel.SetActive(show);
        finalScoreText.text = "Final Score: " + finalScore;
    }

    public void OnRestartButton()
    {
        FindObjectOfType<GameManager>().RestartGame();
    }

    public void OnBackToMenuButton()
    {
        FindObjectOfType<GameManager>().BackToMenu();
    }

}
