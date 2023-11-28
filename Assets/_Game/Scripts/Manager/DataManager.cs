using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private int level;
    
    #region Unity Functions
    protected void Awake()
    {
        level = PlayerPrefs.GetInt("Level", 1);
    }

    private void Start()
    {
        
    }

    #endregion
    
    #region Other Functions
    
    private void SetLevel (int value)
    {
        level = value;

        PlayerPrefs.SetInt("Level", level);
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
