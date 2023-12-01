using System.Collections;
using System.Collections.Generic;
using _Game.Level;
using _Game.Manager;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;
    
    public void NextLevelButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.LoadCurrentLevel();
        Close(0);
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0);
    }
}
