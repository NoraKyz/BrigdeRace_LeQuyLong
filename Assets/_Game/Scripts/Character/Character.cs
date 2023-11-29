using System.Collections;
using System.Collections.Generic;
using _Framework;
using _Game.Brick;
using _Game.Framework.Event;
using _Game.Utils;
using UnityEngine;
using Utils;


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
                Character character = Cache<Character>.GetScript(other);
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
        private void OnNextStage(GateIn gateIn)
        {
            CurrentStageId = gateIn.StageId;
            StartCoroutine(MovePosition(TF.position + Vector3.forward * 2f, 0.2f));
        }
        protected virtual void DropBrick()
        {
            while (BrickAmount > 0)
            {
                SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position, Quaternion.identity);
                RemoveBrick();
            }
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
                BridgeBrick bridgeBrick = Cache<BridgeBrick>.GetScript(hit.collider);

                if (bridgeBrick.ColorType != colorType && BrickAmount > 0)
                {
                    bridgeBrick.ChangeColor(colorType);
                    RemoveBrick();
                }
                
                if (bridgeBrick.ColorType != colorType && BrickAmount == 0 && model.forward.z > 0)
                {
                    return false;
                }
            }
            
            return true;
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
        public void RemoveBrick()
        {
            SimplePool.Despawn(_bricks.Pop());
        }
        public virtual void OnWinPos()
        {
            ChangeAnim(CharacterAnimName.Win);
        }
        public IEnumerator MovePosition(Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = TF.position;
        
            while (time < duration)
            {
                TF.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        
            TF.position = targetPosition;
        }
        public bool CheckGate()
        {
            // UNDONE: Check gate
            
            if (Physics.Raycast(TF.position, model.forward, out var hit, 1f, gateLayer))
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
