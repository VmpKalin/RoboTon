using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui.WindowSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWindow : Window
{
    public GameObject HighScoreAlert;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        HighScoreAlert.SetActive(false);
        highScoreText.text = highScore.ToString();
        scoreText.text = score.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

#if UNITY_EDITOR
    [MenuItem("Match three/Reset high score")]
    static void ResetHighScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
        PlayerPrefs.SetInt("score", 0);
    }
#endif
}
