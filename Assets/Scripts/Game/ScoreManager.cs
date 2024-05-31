using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScoreManager: MonoBehaviour
{
    private const string HIGHSCORE_KEY = "highScore";

    public static ScoreManager Instance { get; private set; }
    public event Action<int, int> CurrentScoreChanged;
    
    private int _currentScore;

    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            var oldScore = _currentScore;
            _currentScore = value;
            CurrentScoreChanged?.Invoke(oldScore, value);
        }
    }

    public bool CheckAndSetHighScore(int score)
    {
        if (score <= HighScore) return false;
        HighScore = score;
        if(ProfileManager.Instance)ProfileManager.Instance.UpdateHighScore(score);
        return true;
    }

    public int HighScore { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}