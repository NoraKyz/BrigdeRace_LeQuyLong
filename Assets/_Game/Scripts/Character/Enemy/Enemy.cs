using System;
using _Game.Pattern.StateMachine;
public class Enemy : Character
{
    private IState<Enemy> _currentState;
    
    private IdleState _idleState;
    private CollectState _collectState;
    private MoveToFinishPointState _moveToFinishPointState;
    private FallState _fallState;
    private void Awake()
    {
        _idleState = new IdleState();
        _collectState = new CollectState();
        _moveToFinishPointState = new MoveToFinishPointState();
        _fallState = new FallState();
    }

    protected override void OnInit()
    {
        base.OnInit();
        ChangeState(_idleState);
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

}


