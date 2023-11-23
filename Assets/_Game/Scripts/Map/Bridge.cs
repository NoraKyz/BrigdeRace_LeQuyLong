using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Bridge : MonoBehaviour
{
    [SerializeField] private int bridgeLength;
    [SerializeField] private List<BrigdeBrick> listBricks = new List<BrigdeBrick>();
    [SerializeField] private Vector3 offset;

    private Vector3 _spawnPos;

    private void Awake()
    {
        offset.y = Constants.BridgeBrickHeight;
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        SpawnBridge();
    }

    private void SpawnBridge()
    {
        Vector3 cachePos = transform.position;
        for(int i = 0; i < bridgeLength; i++)
        {
            _spawnPos.Set(cachePos.x + offset.x * i, cachePos.y + offset.y * i, cachePos.z + offset.z * i);
            BrigdeBrick brick = SimplePool.Spawn<BrigdeBrick>(PoolType.BrigdeBrick, _spawnPos, Quaternion.identity, transform);
            
            brick.ChangeColor(ColorType.Blue);
            listBricks.Add(brick);
        }
    }
}