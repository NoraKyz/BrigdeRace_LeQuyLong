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
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Instance.ClearCurrentLevel();
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<GamePlay>();
        Close(0);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
