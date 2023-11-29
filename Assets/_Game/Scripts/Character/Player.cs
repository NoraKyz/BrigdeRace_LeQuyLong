using _Game.Framework.Event;
using UnityEngine;
using Utils;

namespace _Game.Character
{
    public class Player : Character
    {
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
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            if (IsFalling)
            {
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
        public override void OnWinPos()
        {
            base.OnWinPos();
            this.PostEvent(EventID.PlayerWin);
        }
        private bool CanMove(Vector3 nextPoint)
        {
            return CheckStair(nextPoint) && CheckGate(); 
        }
    }
}
