using System.Collections;
using System.Collections.Generic;
using _Game.Brick;
using _Game.Framework.Event;
using _Game.Utils;
using UnityEngine;
using Utils;

namespace _Game.Character
{
    public class Character : ObjectColor
    {
        [Header("Layer")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask stairLayer;
        [SerializeField] private LayerMask gateLayer;
    
        [Header("Components")]
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform model;
    
        [Header("Properties")]
        [SerializeField] protected Stage currentStage;
        [SerializeField] protected Transform brickHolder;
    
        [HideInInspector] public bool isFalling = false;
        protected Stack<CharacterBrick> bricks = new Stack<CharacterBrick>();
        protected string currentAnimName;
    
        //public event Action OnMovePositionComplete;
        public int BrickAmount => bricks.Count;
        public int CurrentStageId { get; private set; }
        protected void Start()
        {
            OnInit();
        }
        protected override void OnInit()
        {
            base.OnInit();
            ChangeAnim(CharacterAnimName.Idle);
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character character = other.GetComponent<Character>();
                if(character.BrickAmount > BrickAmount && BrickAmount > 0)
                {
                    DropBrick();
                }
            }
        }
        protected virtual void Move() { }
        private Vector3 GetNextBrickPosInHolder()
        {
            if(BrickAmount == 0)
            {
                return Vector3.zero;
            }
        
            return bricks.Peek().transform.localPosition + Vector3.up * Constants.CharacterBrickSize.y * 1.2f;
        }
        protected void RotateTowardMoveDirection(Vector3 nextPoint)
        {
            Vector3 direction = nextPoint - transform.position;
            direction.y = 0;
            model.forward = direction;
        }
        protected Vector3 CheckGround(Vector3 nextPoint)
        {
            RaycastHit hit;

            if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
            {
                return hit.point + Vector3.up;
            }

            return transform.position;
        }
        protected bool CanMove(Vector3 nextPoint)
        {
            return CheckStair(nextPoint) && CheckGate(); 
        }
        protected virtual void OnNextStage(GateIn gateIn)
        {
            CurrentStageId = gateIn.StageId;
            StartCoroutine(MovePosition(TF.position + Vector3.forward * 2f, 0.2f));
        }
        public void ChangeAnim(string animName)
        {
            if (currentAnimName == animName)
            {
                return;
            }
        
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
        public virtual void AddBrick()
        {
            CharacterBrick brick = SimplePool.Spawn<CharacterBrick>(
                PoolType.CharacterBrick, 
                GetNextBrickPosInHolder(),
                Quaternion.identity, 
                brickHolder
            );
            brick.ChangeColor(colorType);
            bricks.Push(brick);
        }
        public virtual void RemoveBrick()
        {
            SimplePool.Despawn(bricks.Pop());
        }
        public virtual void DropBrick()
        {
            while (BrickAmount > 0)
            {
                SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position, Quaternion.identity);
                RemoveBrick();
            }
        }
        public virtual void OnWinPos()
        {
            ChangeAnim(CharacterAnimName.Win);
        }
        public IEnumerator MovePosition(Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = transform.position;
        
            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        
            transform.position = targetPosition;
        }
        public void SetStage(Stage stage)
        {
            currentStage = stage;
        }
        public bool CheckStair(Vector3 nextPoint)
        {
            RaycastHit hit;
        
            if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
            {
                BridgeBrick bridgeBrick = hit.collider.GetComponent<BridgeBrick>();

                if (bridgeBrick.colorType != colorType && BrickAmount > 0)
                {
                    bridgeBrick.ChangeColor(colorType);
                    RemoveBrick();
                }

                if (bridgeBrick.colorType != colorType && BrickAmount == 0 && model.forward.z > 0)
                {
                    return false;
                }
            }

            return true;
        }
        public bool CheckGate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, model.transform.forward, out hit, 1f, gateLayer))
            {
                GateIn gateIn = hit.collider.GetComponent<GateIn>();

                if (gateIn.StageId != CurrentStageId)
                {
                    OnNextStage(gateIn);
                    this.PostEvent(EventID.CharacterOnNextStage, this);
                }
                else
                {
                    return false;
                }
            }
        
            return true;
        }
    }
}
