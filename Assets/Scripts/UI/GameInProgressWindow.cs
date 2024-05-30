using System;
using Ui.WindowSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameInProgressWindow: Window
    {
        [SerializeField] private TextMeshProUGUI _movesText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
        }

        protected override void OnShown()
        {
            if (ScoreManager.Instance)
            {
                ScoreManager.Instance.CurrentScoreChanged += OnCurrentScoreChanged;
                _scoreText.text =  ScoreManager.Instance.CurrentScore.ToString();
            }

            if (GridManager.Instance)
            {
                GridManager.Instance.NumMovesChanged += OnNumMovesChanged;
                _movesText.text =  GridManager.Instance.NumMoves.ToString();
            }
            
            GridManager.Instance.StartNewGame();
        }

        protected override void OnHidden()
        {
            if(ScoreManager.Instance) ScoreManager.Instance.CurrentScoreChanged -= OnCurrentScoreChanged;
            if(GridManager.Instance) GridManager.Instance.NumMovesChanged -= OnNumMovesChanged;
        }

        private void OnCurrentScoreChanged(int oldValue, int newValue)
        {
            _scoreText.text = newValue.ToString();
        }
        private void OnNumMovesChanged(int oldValue, int newValue)
        {
            _movesText.text = newValue.ToString();
        }
    }
}