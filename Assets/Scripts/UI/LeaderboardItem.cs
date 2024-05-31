using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    [SerializeField] private Image _bgImage;
    
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoresText;
    
    [SerializeField] private Color _evenColor = Color.grey;
    [SerializeField] private Color _oddColor = Color.grey;

    public void Setup(string name, int scores)
    {
        _bgImage.color = transform.GetSiblingIndex() % 2 == 0 ? _evenColor : _oddColor;

        _nameText.text = name;
        _scoresText.text = scores.ToString();
    }
}
