using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Framework.Event;
using _Game.Utils;
using UnityEngine;
using Utils;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startCharPos;
    
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Stage> stages = new List<Stage>();

    private int _charAmount;
    private List<ColorType> _colorTypes;
    
    private void Awake()
    {
        _charAmount = characters.Count;
    }
    private void Start()
    {
        _colorTypes = LevelManager.Instance.GetRandomListColor(characters.Count);
        characters = Utilities.RandomList(characters, characters.Count);
        
        SetStartCharPos();
        SetColorChar();
        SetColorsStartStage();
        SetStageAllChar(0);
        SetMaxPlayerStage();
        
        EventRegister();
    }
    
    private void EventRegister()
    {
        EventManager.Instance.RegisterListener(EventID.CharacterOnNextStage, (param) => OnCharacterNextStage((Character)param));
    }
    
    private void OnCharacterNextStage(Character character)
    {
        stages[character.CurrentStageId].OnCharacterEnter(character);
    }
    
    private void SetStartCharPos()
    {
        for(int i = 0; i < _charAmount; i++)
        {
            characters[i].transform.position = startCharPos.position + Vector3.right * (1f * Constants.MaxDistanceStartCharPos / (_charAmount - 1)) * i;
        }
    }
    private void SetColorChar()
    {
        for (int i = 0; i < _charAmount; i++)
        {
            characters[i].ChangeColor(_colorTypes[i]);
        }
    }

    private void SetStageAllChar(int id)
    {
        for(int i = 0; i < _charAmount; i++)
        {
            characters[i].SetStage(stages[id]);
        }
    }
    
    private void SetMaxPlayerStage()
    {
        for(int i = 0; i < stages.Count; i++)
        {
            stages[i].SetMaxPlayer(_charAmount);
        }
    }
    private void SetColorsStartStage()
    {
        stages[0].SetListColor(_colorTypes);
    }
}
