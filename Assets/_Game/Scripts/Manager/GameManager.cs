using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using _Game.Framework.Event;
using _Game.Manager;
using _UI.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    private static GameState _gameState = GameState.MainMenu;

    // Start is called before the first frame update
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        ChangeState(GameState.MainMenu);

        UIManager.Instance.OpenUI<MainMenu>();
        
        RegisterEvent();
    }

    private void RegisterEvent()
    {
        this.RegisterListener(EventID.PlayerWin, (param) => OnPlayerWin());
        this.RegisterListener(EventID.PlayerLose, (param) => OnPlayerLose());
    }

    public static void ChangeState(GameState state)
    {
        _gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return _gameState == state;
    }
    
    private void OnPlayerWin()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<Win>();
        DataManager.Instance.SetNexLevel();
    }
    
    private void OnPlayerLose()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<Lose>();
    }
}
