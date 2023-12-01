using System.Collections;
using System.Collections.Generic;
using _Game.Level;
using _Game.Manager;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.LoadCurrentLevel();
        UIManager.Instance.OpenUI<GamePlay>();
        Close(0);
    }
}
