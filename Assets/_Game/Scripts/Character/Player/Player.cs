using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Player : Character
{
    [Header("Controller")] 
    [SerializeField] private FloatingJoystick joystick;
    
    private Vector3 _inputDirection;
    
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
 
}
