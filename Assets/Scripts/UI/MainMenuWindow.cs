using System.Collections;
using System.Collections.Generic;
using Ui.WindowSystem;
using UnityEngine;

namespace UI
{
    public class MainMenuWindow : Window
    {
        public void StartGame()
        {
            MenuRouter.Instance.Router.Show<GameInProgressWindow>();
        }

        public void OpenProfileMenu()
        {
            MenuRouter.Instance.Router.Show<ProfileWindow>();
        }
        
        public void OpenLeaderboardMenu()
        {
            MenuRouter.Instance.Router.Show<LeaderboardWindow>();
        }
    }
}