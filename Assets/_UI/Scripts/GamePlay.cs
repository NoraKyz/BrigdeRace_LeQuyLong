using _Game.Level;
using _Game.Manager;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public Text level;
    
    public void SettingButton()
    {
        GameManager.ChangeState(GameState.Setting);
        UIManager.Instance.OpenUI<Setting>();
    }
}
