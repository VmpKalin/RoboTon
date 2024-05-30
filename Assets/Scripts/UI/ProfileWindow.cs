using System.Collections.Generic;
using TMPro;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class ProfileWindow: Window
    {
        [SerializeField] private TextMeshProUGUI _usernameText;
        [SerializeField] private TextMeshProUGUI _idText;
        
        protected override void OnSetInfoToShow(object infoToShow)
        {
            string username = URLParameters.GetSearchParameters().GetValueOrDefault("username", "username");
            string id = URLParameters.GetSearchParameters().GetValueOrDefault("id", "id");
            _usernameText.text = username;
            _idText.text = id;
        }
    }
}