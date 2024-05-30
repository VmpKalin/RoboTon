using System.Collections;
using System.Collections.Generic;
using Ui.WindowSystem;
using UnityEngine;

public class MainMenuWindow : Window
{
    
    public void StartGame()
    {
        MenuRouter.Instance.Router.Show<GameInProgressWindow>();
    }
}
