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

        [Header("Components")]
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform model;
        [SerializeField] protected Transform brickHolder;
        
        [Header("Layer")]
        [SerializeField] private LayerMask stairLayer;
        
        private Stack<CharacterBrick> _bricks = new Stack<CharacterBrick>();
        private string _currentAnimName;

        public bool IsFalling { get; set; }
        public int BrickAmount => _bricks.Count;
        public int CurrentStageId { get; private set; } = 0;
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
        private Vector3 GetNextBrickPosInHolder()
        {
            if(BrickAmount == 0)
            {
                return Vector3.zero;
            }
        
            return _bricks.Peek().TF.localPosition + Vector3.up * Constants.CharacterBrickSize.y * OffsetCharBrick;
        }
        protected override void OnInit()
        {
            base.OnInit();
            ChangeAnim(CharacterAnimName.Idle);
        }
        protected virtual void DropBrick()
        {
            while (BrickAmount > 0)
            {
                SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position, Quaternion.identity);
                RemoveBrick();
            }
        }
        public bool CheckStair(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, out var hit, 2f, stairLayer))
            {
                BridgeBrick bridgeBrick = Cache<BridgeBrick>.GetScript(hit.collider);

                if (bridgeBrick.ColorType != ColorType && BrickAmount == 0 && model.forward.z > 0)
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
            brick.ChangeColor(ColorType);
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
        public virtual void OnEnterStage(int stageId)
        {
            CurrentStageId = stageId;
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
    }
}
