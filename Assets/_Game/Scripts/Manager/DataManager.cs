using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private int levelId;

    public int LevelId => levelId;
    
    #region Unity Functions
    protected void Awake()
    {
        levelId = PlayerPrefs.GetInt("Level", 1);
    }

    #endregion
    
    #region Other Functions
    
    private void SetLevel (int value)
    {
        levelId = value;

        PlayerPrefs.SetInt("Level", levelId);
    }
    
    private int GetNextLevel(int currentLevel)
    {
        if(currentLevel + 1 > Constants.MaxLevel)
        {
            return 1;
        }
        
        return currentLevel + 1;
    }

    #endregion
}
