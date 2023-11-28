using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        LevelManager.Instance.ClearCurrentLevel();
        UIManager.Instance.OpenUI<MainMenu>();
        Close(0);
    }
}
