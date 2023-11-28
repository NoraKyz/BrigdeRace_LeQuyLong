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

    protected void Awake()
    {
        levelId = PlayerPrefs.GetInt("Level", 1);
    }
    public void SetNexLevel()
    {
        if(levelId + 1 > Constants.MaxLevel)
        {
            levelId = 1;
        }
        
        levelId += 1;
        
        PlayerPrefs.SetInt("Level", levelId);
    }
}
