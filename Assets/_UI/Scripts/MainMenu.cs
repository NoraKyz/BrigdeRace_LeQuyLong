using System;
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
        UIManager.Instance.OpenUI<GamePlay>();
        LevelManager.Instance.LoadCurrentLevel();
        Close(0);
    }

    private void OnEnable()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Instance.CloseUI<GamePlay>();
        LevelManager.Instance.ClearCurrentLevel();
    }
}
