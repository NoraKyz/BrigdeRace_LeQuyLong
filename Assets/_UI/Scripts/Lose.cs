using System.Collections;
using System.Collections.Generic;
using _Game.Level;
using _UI.Scripts.UI;
using UnityEngine;
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
        LevelManager.Instance.ClearCurrentLevel();
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0);
    }
}
