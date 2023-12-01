using _Framework.Event.Scripts;
using _Framework.Singleton;
using _Game.Level;
using UnityEngine;

namespace _Game.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        //[SerializeField] UserData userData;
        //[SerializeField] CSVData csv;
        private static GameState _gameState;
        public static void ChangeState(GameState state)
        {
            _gameState = state;
        }
        public static bool IsState(GameState state) => _gameState == state;
        private void Awake()
        {
            // Tranh viec nguoi choi cham da diem vao man hinh
            Input.multiTouchEnabled = false;
            // Target frame rate ve 60 fps
            Application.targetFrameRate = 60;
            // Tranh viec tat man hinh
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Xu tai tho
            int maxScreenHeight = 1280;
            float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
            }
            
            //csv.OnInit();
            //userData?.OnInitData();
        }
        private void Start()
        {
            ChangeState(GameState.MainMenu);
            UIManager.Instance.OpenUI<MainMenu>();
            UIManager.Instance.OpenUI<GamePlay>();
            UIManager.Instance.CloseUI<GamePlay>();
        
            RegisterEvent();
        }
        private void RegisterEvent()
        {
            this.RegisterListener(EventID.GameFinish, (param) => OnGameFinish((GameResult) param));
        }
        private void OnGameFinish(GameResult result)
        {
            ChangeState(GameState.MainMenu);
        
            if (result == GameResult.Win)
            {
                OnPlayerWin();
            }
            else
            {
                OnPlayerLose();
            }
        }
        private void OnPlayerWin()
        {
            UIManager.Instance.OpenUI<Win>();
            DataManager.Instance.SetNexLevel();
        }
        private void OnPlayerLose()
        {
            UIManager.Instance.OpenUI<Lose>();
        }
        
        private void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        private void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
