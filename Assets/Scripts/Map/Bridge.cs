using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < bridgeLength; i++)
        {
            Instantiate(bridgeBrickPrefab, transform.position + offset * i, Quaternion.identity, transform);
        }
    }
}
