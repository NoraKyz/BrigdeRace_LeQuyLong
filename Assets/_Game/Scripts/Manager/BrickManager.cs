using System;
using System.Collections;
using System.Collections.Generic;
using Pattern;
using UnityEngine;

public class BrickManager: MonoBehaviour
{
    [Header("List Brick")]
    [SerializeField] private KeyValuePair<PlatformBrick, Transform> platformBrick;
    [SerializeField] private CharacterBrick characterBrick;
    [SerializeField] private BrigdeBrick brigdeBrick;
    [SerializeField] private DropBrick dropBrick;
    
    
    private void Awake()
    {
        
    }
    
}
