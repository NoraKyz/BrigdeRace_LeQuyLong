using _Game.Framework.Event;
using _Game.Framework.StateMachine;
using _Game.Pattern.StateMachine;
using _Game.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Game.Character
{
    public class Enemy : Character
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent navMeshAgent;
    
        [Header("Properties")]
        [SerializeField] private BotConfig botConfig;
        [SerializeField] private Transform finishPos;
    
        private bool _isGoingToFinishPoint = false;
        private IState<Enemy> _currentState;
        private Vector3 _destination;
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;
        public BotConfig BotConfig => botConfig;
        public Vector3 NextPosition => navMeshAgent.nextPosition;

        #region States
        public IdleState IdleState { get; private set; }
        public CollectState CollectState { get; private set; }
        public MoveToFinishPointState MoveToFinishPointState { get; private set; }
        public FallState FallState { get; private set; }

        #endregion
        private void Awake()
        {
            IdleState = new IdleState();
            CollectState = new CollectState();
            MoveToFinishPointState = new MoveToFinishPointState();
            FallState = new FallState();
        }
        private void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnExecute(this);
            }
        }
        private void MoveToPosition(Vector3 position)
        {
            _destination = position;
            navMeshAgent.SetDestination(_destination);
            ChangeAnim(CharacterAnimName.Run);
        }
        private void RandomChanceMoveToFinishPos()
        {
            if (_isGoingToFinishPoint)
            {
                return;
            }
        
            if (Utilities.Chance(botConfig.chanceToFinishPoint))
            {
                _isGoingToFinishPoint = true;
                ChangeState(MoveToFinishPointState);
            }
            else
            {
                MoveToRandomBrick();
            }
        }
        protected override void OnInit()
        {
            base.OnInit();
        
            navMeshAgent.speed = botConfig.moveSpeed;
            ChangeState(IdleState);
        }
        protected override void DropBrick()
        {
            base.DropBrick();
            ChangeState(FallState);
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
        public void MoveToFinishPos()
        {
            MoveToPosition(finishPos.position);   
        }
        public void MoveToRandomBrick()
        {
            // UNDONE: Get random brick position
            // Vector3? brickPos = ;
            //
            // if (brickPos != null)
            // {
            //     MoveToPosition((Vector3)brickPos);
            // }
            // else
            // {
            //     ChangeState(MoveToFinishPointState);
            // }
        }   
        public override void AddBrick()
        {
            base.AddBrick();
            RandomChanceMoveToFinishPos();
        }
        public void StopMove()
        {
            MoveToPosition(TF.position);
        }
        public void NotEnoughBrick()
        {
            _isGoingToFinishPoint = false;
            ChangeState(CollectState);
        }
        public override void OnWinPos()
        {
            base.OnWinPos();
            this.PostEvent(EventID.PlayerLose);
        }
    }
}


