using System.Collections.Generic;
using _Framework.Singleton;
using _Game.Manager;
using UnityEngine;

namespace _Game.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private List<GameObject> levelPrefabs = new List<GameObject>();
        
        private GameObject _currentLevel;

        public void LoadCurrentLevel()
        {
            LoadLevel(DataManager.Instance.CurrentLevelId);
        }
        private void LoadLevel(int id)
        {
            if (_currentLevel != null)
            {
                ClearCurrentLevel();
            }
        
            _currentLevel = Instantiate(levelPrefabs[id], transform);
        }
        public void ClearCurrentLevel()
        {
            if (_currentLevel != null)
            {
                SimplePool.CollectAll();
                Destroy(_currentLevel);
            }
            
            _currentLevel = null;
        }
    }
}
