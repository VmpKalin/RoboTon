using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private GameObject _highScoreAlert;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private GameObject _scoresPanel;

        protected override void OnSetInfoToShow(object infoToShowObj)
        {
            if (infoToShowObj is not InfoToShow infoToShow)
            {
                _scoresPanel.SetActive(false);
                return;
            }
        
            _scoresPanel.SetActive(true);
            
            _highScoreAlert.SetActive(infoToShow.IsNewHighScore);
            _highScoreText.text = infoToShow.Highscore.ToString();
            _scoreText.text = infoToShow.Score.ToString();
        }

        public struct InfoToShow
        {
            public int Score;
            public int Highscore;
            public bool IsNewHighScore;
        }
        
        public void StartGame()
        {
            MenuRouter.Instance.Router.Show<GameInProgressWindow>();
        }

        public void OpenProfileMenu()
        {
            PopupRouter.Instance.Router.Show<ProfileWindow>();
        }
        
        public void OpenLeaderboardMenu()
        {
            PopupRouter.Instance.Router.Show<LeaderboardWindow>();
        }
    }
}