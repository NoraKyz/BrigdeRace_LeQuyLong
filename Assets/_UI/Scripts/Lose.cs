using System;
using _Game.Level;
using _Game.Manager;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void RestartButton()
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
