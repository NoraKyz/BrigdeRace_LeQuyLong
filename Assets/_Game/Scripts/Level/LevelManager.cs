using System.Collections;
using System.Collections.Generic;
using _Game.Utils;
using UnityEngine;
using Utils;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelPrefabs = new List<GameObject>();
    [SerializeField] private List<ColorType> colorTypes = new List<ColorType>();
    
    private int currentLevelID;
    private GameObject currentLevel;

    private void LoadLevel(int id)
    {
        currentLevelID = id;
    
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        
        currentLevel = Instantiate(levelPrefabs[id - 1], transform);
    }
    
    public void LoadCurrentLevel()
    {
        LoadLevel(DataManager.Instance.LevelId);
    }
    
    public void ClearCurrentLevel()
    {
        if (currentLevel != null)
        {
            SimplePool.CollectAll();
            Destroy(currentLevel);
        }
    }
}
