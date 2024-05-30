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
    private int _highScore;

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
        return true;
    }

    public int HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, value);
        }
    }

    private void Awake()
    {
        Instance = this;
        _highScore = PlayerPrefs.HasKey(HIGHSCORE_KEY) ? PlayerPrefs.GetInt(HIGHSCORE_KEY) : 0;
    }
    
    
#if UNITY_EDITOR
    [MenuItem("Tools/Match three/Reset high score")]
    static void ResetHighScore()
    {
        PlayerPrefs.SetInt(HIGHSCORE_KEY, 0);
    }
#endif
}