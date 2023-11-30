using System.Collections;
using System.Collections.Generic;
using _Framework;
using _Game.Brick;
using _Game.Map;
using _Game.Utils;
using UnityEngine;
using Utils;


namespace _Game.Character
{
    public class Character : ObjectColor
    {
        private const float OffsetCharBrick = 1.2f;

        [Header("Components")]
        [SerializeField] protected Stage currentStage;
        [SerializeField] protected Animator anim;
        [SerializeField] protected Transform model;
        [SerializeField] protected Transform brickHolder;
        
        [Header("Layer")]
        [SerializeField] private LayerMask stairLayer;
        
        private Stack<CharacterBrick> _listBricks = new Stack<CharacterBrick>();
        private string _currentAnimName;

        public bool IsFalling { get; private set; }
        public int BrickAmount => _listBricks.Count;
        public int CurrentStageId { get; private set; }
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
        protected void Start()
        {
            OnInit();
        }
        protected override void OnInit()
        {
            base.OnInit();
            
            IsFalling = false;
            CurrentStageId = 0;
            ChangeAnim(CharacterAnimName.Idle);
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character character = Cache<Character>.GetComponent(other);
                
                if(BrickAmount < character.BrickAmount && BrickAmount > 0)
                {
                    DropBrick();
                    StartCoroutine(OnFall());
                }
            }
        }
        protected virtual void DropBrick()
        {
            while (BrickAmount > 0)
            {
                SimplePool.Spawn<DropBrick>(PoolType.DropBrick, transform.position, Quaternion.identity);
                RemoveBrick();
            }
        }
        private IEnumerator OnFall()
        {
            IsFalling = true;
            ChangeAnim(CharacterAnimName.Fall);
            yield return new WaitForSeconds(Constants.StunTime);
            IsFalling = false;
        }
        public bool CheckStair(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, out var hit, 2f, stairLayer))
            {
                BridgeBrick bridgeBrick = Cache<BridgeBrick>.GetComponent(hit.collider);

                if (ColorType != bridgeBrick.ColorType && BrickAmount == 0 && model.forward.z > 0)
                {
                    return false;
                }
            }

            return true;
        }
        public void AddBrick()
        {
            CharacterBrick brick = SimplePool.Spawn<CharacterBrick>(
                PoolType.CharacterBrick, 
                GetNextBrickPosInHolder(),
                Quaternion.identity, 
                brickHolder
            );
            
            brick.ChangeColor(ColorType);
            _listBricks.Push(brick);
        }
        private Vector3 GetNextBrickPosInHolder()
        {
            if(BrickAmount == 0)
            {
                return Vector3.zero;
            }
        
            return _listBricks.Peek().TF.localPosition + Vector3.up * Constants.CharacterBrickSize.y * OffsetCharBrick;
        }
        public void RemoveBrick()
        {
            SimplePool.Despawn(_listBricks.Pop());
        }
        public virtual void OnWinPos() { }
        public virtual void SetCurrentStageID(int stageId)
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
        public void SetCurrentStage(Stage stage)
        {
            currentStage = stage;
        }
    }
}
