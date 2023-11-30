using System.Collections;
using System.Collections.Generic;
using _Game.Level;
using _Game.Manager;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        LevelManager.Instance.LoadCurrentLevel();
        GameManager.ChangeState(GameState.GamePlay);
        Close(0);
    }
}
