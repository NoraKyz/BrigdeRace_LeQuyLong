using System;
using System.Collections.Generic;
using _Game.Framework.StateMachine;
using _Game.Pattern.StateMachine;
using _Game.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Character
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    [Header("Config")] 
    [SerializeField] public BotConfig botConfig;
    
    [Header("Properties")]
    [SerializeField] private Transform finishPoint;
    [SerializeField] private Stage currentStage;
    
    private IState<Enemy> _currentState;
    private List<Vector3> _listBrickPos = new List<Vector3>();
    public BotConfig BotConfig => botConfig;
    public IdleState IdleState { get; private set; }
    public CollectState CollectState { get; private set; }
    public MoveToFinishPointState MoveToFinishPointState { get; private set; }
    public FallState FallState { get; private set; }

    private void Awake()
    {
        IdleState = new IdleState();
        CollectState = new CollectState();
        MoveToFinishPointState = new MoveToFinishPointState();
        FallState = new FallState();
    }
    protected override void OnInit()
    {
        base.OnInit();
        
        navMeshAgent.speed = botConfig.moveSpeed; 
        ChangeState(IdleState);
    }
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState<Enemy> state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }
    public void MoveToPosition(Vector3 position)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(position);
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }
    public void UpdateListBrickPos()
    {
        _listBrickPos = currentStage.GetListPosBrickTakeable(colorType);
    }
}


