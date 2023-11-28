using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Brick;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class Bridge : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int bridgeCount;
    [SerializeField] private List<BridgeBrick> listBricks = new List<BridgeBrick>();
    [SerializeField] private Vector3 brickOffset;
    
    [Header("Components")]
    [SerializeField] private Transform slope;
    [SerializeField] private Transform rope;
    

    private Vector3 _spawnPos;
    private static readonly Vector3 BridgeBrickSize = Constants.BridgeBrickSize;

    private void Awake()
    {
        brickOffset.y = BridgeBrickSize.y;
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        SpawnBridge();
        
        SetSlope();
        
        SetRope();
    }

    private void SpawnBridge()
    {
        for(int i = 0; i < bridgeCount; i++)
        {
            _spawnPos.Set(0, brickOffset.y * i, brickOffset.z * i + BridgeBrickSize.z / 2);
            
            BridgeBrick brick = SimplePool.Spawn<BridgeBrick>(PoolType.BrigdeBrick, _spawnPos, Quaternion.identity, transform);
            listBricks.Add(brick);
        }
    }

    private void SetSlope()
    {
        slope.localScale = new Vector3(0.2f, 1, 0.067f * bridgeCount);
        
        float slopeAngle = -Mathf.Atan(BridgeBrickSize.y / brickOffset.z);
        slope.localRotation = Quaternion.Euler(slopeAngle * Mathf.Rad2Deg, 0, 0);
        
        float slopeLength = Mathf.Sqrt(BridgeBrickSize.y * BridgeBrickSize.y + brickOffset.z * brickOffset.z) * bridgeCount;
        slope.localPosition = new Vector3(0, -slopeLength / 2 * Mathf.Sin(slopeAngle), slopeLength / 2 * Mathf.Cos(slopeAngle));
    }

    private void SetRope()
    {
        // TODO: complete code
    }
}