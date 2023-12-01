using System;
using _Game.Level;
using _Game.Manager;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void RestartButton()
    {
        LevelManager.Instance.LoadCurrentLevel();
        GameManager.ChangeState(GameState.GamePlay);
        Close(0);
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<GamePlay>();
        Close(0);
    }
}
