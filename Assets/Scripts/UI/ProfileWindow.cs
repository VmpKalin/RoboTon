using System;
using System.Collections.Generic;
using TMPro;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class ProfileWindow: Window
    {
        [SerializeField] private TextMeshProUGUI _usernameText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _highscoreText;
        [SerializeField] private TextMeshProUGUI _idText;
        [SerializeField] private GameObject _uiToolkitGameObject;

        protected override void OnSetInfoToShow(object infoToShow)
        {
            _usernameText.text = ProfileManager.Instance.CurrentUserInfo.Username;
            _idText.text = ProfileManager.Instance.CurrentUserInfo.user_id;
            
            _highscoreText.text = ProfileManager.Instance.CurrentUserInfo.HighScore.ToString();

            _coinsText.text = ProfileManager.Instance.CurrentUserInfo.Coins.ToString();
        }

        protected override void OnShown()
        {
            _uiToolkitGameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            Hide();
        }
    }
}