using System.Collections;
using System.Collections.Generic;
using _Game.Brick;
using _Game.Framework.Event;
using _Game.Utils;
using UnityEngine;
using Utils;
using Cache = _Framework.Cache;

namespace _Game.Character
{
    public class Character : ObjectColor
    {
        private const float OffsetCharBrick = 1.2f;
        
        [Header("Layer")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask stairLayer;
        [SerializeField] private LayerMask gateLayer;
    
        [Header("Components")]
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform model;
    
        [Header("Properties")]
        [SerializeField] protected Transform brickHolder;
    
        private Stack<CharacterBrick> _bricks = new Stack<CharacterBrick>();
        private string _currentAnimName;
        public bool IsFalling { get; } = false;
        public int BrickAmount => _bricks.Count;
        public int CurrentStageId { get; private set; }
        protected void Start()
        {
            OnInit();
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character character = Cache.GetCharacter(other);
                if(character.BrickAmount > BrickAmount && BrickAmount > 0)
                {
                    DropBrick();
                }
            }
        }
        protected override void OnInit()
        {
            base.OnInit();
            ChangeAnim(CharacterAnimName.Idle);
        }
        private Vector3 GetNextBrickPosInHolder()
        {
            if(BrickAmount == 0)
            {
                return Vector3.zero;
            }
        
            return _bricks.Peek().TF.localPosition + Vector3.up * Constants.CharacterBrickSize.y * OffsetCharBrick;
        }
        protected Vector3 CheckGround(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, out var hit, 2f, groundLayer))
            {
                return hit.point + Vector3.up;
            }

            return TF.position;
        }
        public bool CheckStair(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, out var hit, 2f, stairLayer))
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
        protected virtual void OnNextStage(GateIn gateIn)
        {
            CurrentStageId = gateIn.StageId;
            StartCoroutine(MovePosition(TF.position + Vector3.forward * 2f, 0.2f));
        }
        public void ChangeAnim(string animName)
        {
            if (_currentAnimName == animName)
            {
                return;
            }
        
            anim.ResetTrigger(animName);
            _currentAnimName = animName;
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
            _bricks.Push(brick);
        }
        public virtual void RemoveBrick()
        {
            SimplePool.Despawn(_bricks.Pop());
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
