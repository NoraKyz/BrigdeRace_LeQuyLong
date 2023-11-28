using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startCharPoint;
    [SerializeField] private List<Transform> characters = new List<Transform>();

    private void Start()
    {
        SetStartCharPoint();
    }
    
    private void SetStartCharPoint()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].position = startCharPoint.position + Vector3.right * i * (Constants.MaxDistanceStartCharPos / 5f);
        }
    }
}
