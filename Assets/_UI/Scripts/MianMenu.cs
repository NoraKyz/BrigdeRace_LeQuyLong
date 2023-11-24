using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        Close(0);
    }
}
