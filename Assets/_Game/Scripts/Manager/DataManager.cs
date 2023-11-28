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
        levelId = levelId + 1 > Constants.MaxLevel ? 1 : levelId + 1;
        PlayerPrefs.SetInt("Level", levelId);
    }
}
