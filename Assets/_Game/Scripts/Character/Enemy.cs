using _Framework.Event.Scripts;
using _Framework.StateMachine;
using _Game.Manager;
using _Game.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Character
{
    public class Enemy : Character
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent navMeshAgent;
    
        [Header("Properties")]
        [SerializeField] private BotConfig botConfig;
        
        private IState<Enemy> _currentState;
        private Vector3 _destination;
        
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;
        public Vector3 NextPosition => navMeshAgent.nextPosition;

        #region States

        private IdleState IdleState { get; set; }
        public CollectState CollectState { get; private set; }
        private MoveToFinishPointState MoveToFinishPointState { get; set; }
        private FallState FallState { get; set; }

        #endregion
        protected override void OnInit()
        {
            base.OnInit();
        
            navMeshAgent.speed = botConfig.moveSpeed;
            ChangeState(IdleState);
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

        private void Awake()
        {
            IdleState = new IdleState();
            CollectState = new CollectState();
            MoveToFinishPointState = new MoveToFinishPointState();
            FallState = new FallState();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.RegisterListener(EventID.GameFinish, _ => StopMove());
        }
        
        private void Update()
        {
            if (!GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            
            if (_currentState != null)
            {
                _currentState.OnExecute(this);
            }
        }
        private void MoveToPosition(Vector3 position)
        {
            _destination = position;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(_destination);
            ChangeAnim(CharacterAnimName.Run);
        }
        public void RandomChanceMoveToFinishPos()
        {
            if (Utilities.Chance(botConfig.chanceToFinishPoint))
            {
                ChangeState(MoveToFinishPointState);
            }
            else
            {
                MoveToRandomBrick();
            }
        }
        public void MoveToRandomBrick()
        {
            Vector3? brickPos = currentStage.GetBrickPosTakeAble(ColorType);
            
            if (brickPos != null)
            {
                MoveToPosition((Vector3)brickPos);
            }
            else
            {
                ChangeState(MoveToFinishPointState);
            }
        }   
        protected override void DropBrick()
        {
            base.DropBrick();
            ChangeState(FallState);
        }
        public void MoveToFinishPos()
        {
            Vector3 finishPos = currentStage.GetRandomTargetPos();
            MoveToPosition(finishPos);   
        }
        public void StopMove()
        {
            navMeshAgent.enabled = false;
            ChangeAnim(CharacterAnimName.Idle);
        }
        public override void OnWinPos()
        {
            base.OnWinPos();
            this.PostEvent(EventID.GameFinish, GameResult.Lose);
            ChangeAnim(CharacterAnimName.Win);
        }
    }
}


