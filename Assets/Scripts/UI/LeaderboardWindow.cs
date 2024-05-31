using Ui.WindowSystem;

namespace UI
{
    public class LeaderboardWindow: Window
    {
        
        public void OpenMainMenu()
        {
            MenuRouter.Instance.Router.Show<MainMenuWindow>();
        }
    }
}