using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace _Game.Manager
{
    public class DataManager : Singleton<DataManager>
    {
        [SerializeField] private int currentLevelId;
        public int CurrentLevelId => currentLevelId;
        protected void Awake()
        {
            currentLevelId = PlayerPrefs.GetInt("Level", 0);
        }
        public void SetNexLevel()
        {
            currentLevelId = currentLevelId + 1 >= Constants.MaxLevel ? 0 : currentLevelId + 1;
            PlayerPrefs.SetInt("Level", currentLevelId);
        }
    }
}
