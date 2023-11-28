using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs = new List<GameObject>();

    private int currentLevelID;
    private GameObject currentLevel;
    
    #region Unity Functions

    private void Start()
    {
        // TODO: Subscribe to events
    }

    #endregion

    #region Other Functions

    private void LoadLevel(int id)
    {
        currentLevelID = id;

        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        
        currentLevel = Instantiate(levelPrefabs[id - 1], transform);
    }

    #endregion
}
