using System;
using _Game.Level;
using _Game.Manager;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        Close(0);
    } 
    
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0);
    }
}
