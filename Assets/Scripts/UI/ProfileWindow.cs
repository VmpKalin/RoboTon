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
        [SerializeField] private TMP_InputField _walletInputField;
        [SerializeField] private TextMeshProUGUI _highscoreText;
        [SerializeField] private TextMeshProUGUI _idText;

        private void Start()
        {
            _walletInputField.onEndEdit.AddListener(OnWalletEndEdit);
        }

        private void OnWalletEndEdit(string arg0)
        {
            Debug.Log("new wallet: "+ arg0);
        }

        protected override void OnSetInfoToShow(object infoToShow)
        {
            string username = ProfileManager.Instance.Username;
            string id = ProfileManager.Instance.Id;
            _usernameText.text = username;
            _idText.text = id;
            _highscoreText.text = ScoreManager.Instance.HighScore.ToString();

            _coinsText.text = "0000";
            _walletInputField.text = "1111";
        }

        public void OpenMainMenu()
        {
            MenuRouter.Instance.Router.Show<MainMenuWindow>();
        }
    }
}