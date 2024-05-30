using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui.WindowSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWindow : Window
{
    [SerializeField] private GameObject HighScoreAlert;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    protected override void OnSetInfoToShow(object infoToShowObj)
    {
        if (infoToShowObj is not InfoToShow infoToShow) return;
        
        HighScoreAlert.SetActive(infoToShow.isNewHighScore);
        highScoreText.text = infoToShow.highscore.ToString();
        scoreText.text = infoToShow.score.ToString();
    }

    public void RestartLevel()
    {
        MenuRouter.Instance.Router.Show<GameInProgressWindow>();
    }
    
    public struct InfoToShow
    {
        public int score;
        public int highscore;
        public bool isNewHighScore;
    }
}
