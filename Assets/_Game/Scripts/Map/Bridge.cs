using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject bridgeBrickPrefab;
    [SerializeField] private int bridgeLength;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        SpawnBridge();
    }

    private void SpawnBridge()
    {
        
    }
}