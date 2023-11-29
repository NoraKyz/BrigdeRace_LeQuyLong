using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Character;
using _Game.Framework.Event;
using _Game.Utils;
using UnityEngine;
using Utils;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startCharPos;
    
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Stage> stages = new List<Stage>();
    [SerializeField] private List<ColorType> colorTypes;
    
    private int _charAmount;
    
    
    private void Awake()
    {
        _charAmount = characters.Count;
    }
    private void Start()
    {
        characters = Utilities.RandomList(characters, characters.Count);
    }

}
