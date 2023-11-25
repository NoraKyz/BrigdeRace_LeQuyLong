using System;
using System.Collections.Generic;
using _Game.Framework.Debug;
using _Game.Framework.StateMachine;
using _Game.Pattern.StateMachine;
using _Game.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utils;

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
    public Vector3 Veclocity => navMeshAgent.velocity;
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
        UpdateListBrickPos();
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
    private void MoveToPosition(Vector3 position)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(position);
        ChangeAnim(CharacterAnimName.Run);
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
        ChangeAnim(CharacterAnimName.Idle);
    }
    private void UpdateListBrickPos()
    {
        _listBrickPos = currentStage.GetListPosBrickTakeable(colorType);
    }
    
    public void MoveToFinishPos()
    {
        MoveToPosition(finishPoint.position);   
    }
    
    public void MoveToRandomBrick()
    {
        if (_listBrickPos.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _listBrickPos.Count);
            MoveToPosition(_listBrickPos[randomIndex]);
        }
        else
        {
            ChangeState(MoveToFinishPointState);
        }
    }   

    public override void AddBrick()
    {
        base.AddBrick();
        UpdateListBrickPos();
        
        float random = UnityEngine.Random.Range(0f, 1f);
        if (random >= 0.5f)
        {
            ChangeState(MoveToFinishPointState);
        }
        else
        {
            MoveToRandomBrick();
        }
    }

    public override void DropBrick()
    {
        base.DropBrick();
        ChangeState(FallState);
    }
}


