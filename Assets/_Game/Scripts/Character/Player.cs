using _Game.Character;
using _Game.Framework.Event;
using Camera;
using UnityEngine;
using Utils;

public class Player : Character
{
    [Header("Controller")] 
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] protected float moveSpeed;
    
    private Vector3 _inputDirection;
    
    private void Awake()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<FloatingJoystick>();
        }

        CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();

        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(transform);
        }
    }
    
    private void Update()
    {
        Move();
    }
    protected override void Move()
    {
        if (isFalling)
        {
            return;
        }

        _inputDirection.Set(joystick.Horizontal, 0, joystick.Vertical);
        
        if (_inputDirection != Vector3.zero)
        {
            Vector3 nextPoint = transform.position + _inputDirection.normalized * moveSpeed * Time.deltaTime;
            
            RotateTowardMoveDirection(nextPoint);
            
            if (CanMove(nextPoint))
            {
                transform.position = CheckGround(nextPoint);
            }
            
            ChangeAnim(CharacterAnimName.Run);
        }
        else
        {
            ChangeAnim(CharacterAnimName.Idle);
        }
    }

    public override void OnWinPos()
    {
        base.OnWinPos();
        this.PostEvent(EventID.PlayerWin);
    }
}
