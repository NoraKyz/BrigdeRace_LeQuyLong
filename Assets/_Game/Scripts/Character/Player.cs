using _Framework;
using _Game.Framework.Event;
using _Game.Map;
using _UI.Scripts.UI;
using Camera;
using UnityEngine;
using Utils;

namespace _Game.Character
{
    public class Player : Character
    {
        [SerializeField] private LayerMask gateLayer;
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Controller")] 
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private float moveSpeed;
    
        private Vector3 _inputDirection;
        private void Awake()
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
            
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            cameraFollow.SetTarget(TF);
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            if (IsFalling)
            {
                ChangeAnim(CharacterAnimName.Fall);
                return;
            }

            _inputDirection.Set(joystick.Horizontal, 0, joystick.Vertical);
        
            if (Vector3.Distance(_inputDirection, Vector3.zero) > Constants.MinSwipeDistance)
            {
                Vector3 nextPoint = TF.position + _inputDirection.normalized * (moveSpeed * Time.deltaTime);
            
                RotateTowardMoveDirection(nextPoint);
            
                if (CanMove(nextPoint))
                {
                    TF.position = CheckGround(nextPoint);
                }
            
                ChangeAnim(CharacterAnimName.Run);
            }
            else
            {
                ChangeAnim(CharacterAnimName.Idle);
            }
        }
        private void RotateTowardMoveDirection(Vector3 nextPoint)
        {
            Vector3 direction = nextPoint - TF.position;
            direction.y = 0;
            model.forward = direction;
        }
        private bool CanMove(Vector3 nextPoint)
        {
            return CheckStair(nextPoint) && CheckGate(nextPoint); 
        }
        private Vector3 CheckGround(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, out var hit, 2f, groundLayer))
            {
                return hit.point + Vector3.up;
            }

            return TF.position;
        }
        private bool CheckGate(Vector3 nextPoint)
        {
            if (Physics.Raycast(nextPoint, Vector3.down, 2f, gateLayer))
            {
                return false;
            }
            
            return true;
        }
        public override void OnEnterStage(int stageID)
        {
            base.OnEnterStage(stageID);
            StartCoroutine(MovePosition(TF.position + Vector3.forward * 2f, 0.2f));

        }
        public override void OnWinPos()
        {
            this.PostEvent(EventID.PlayerWin);
            base.OnWinPos();
        }
    }
}
