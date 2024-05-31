using Ui.WindowSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameInProgressWindow: Window
    {
        [SerializeField] private TextMeshProUGUI _movesText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GridManager _gridManager;
        protected override void OnShown()
        {
            if (ScoreManager.Instance)
            {
                ScoreManager.Instance.CurrentScoreChanged += OnCurrentScoreChanged;
                _scoreText.text =  ScoreManager.Instance.CurrentScore.ToString();
            }

            if (_gridManager)
            {
                _gridManager.NumMovesChanged += OnNumMovesChanged;
                _movesText.text =  _gridManager.NumMoves.ToString();
            }
        
            _gridManager.StartNewGame();
        }

        protected override void OnHidden()
        {
            if(ScoreManager.Instance) ScoreManager.Instance.CurrentScoreChanged -= OnCurrentScoreChanged;
            if(_gridManager) _gridManager.NumMovesChanged -= OnNumMovesChanged;
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