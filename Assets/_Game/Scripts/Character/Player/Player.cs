using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Player : Character
{
    [Header("Controller")] 
    [SerializeField] private FloatingJoystick joystick;
    
    private bool _isMoving = false;
    private bool _isFalling = false;
    
    private Vector3 _currentVelocity;
    
    private void Update()
    {
        GetInput();
        
        Move();
    }

    protected override void OnInit()
    {
        base.OnInit();
        ChangeAnim(CharacterAnimName.Idle);
    }
    private void GetInput()
    {
        if (_isFalling)
        {
            return;
        }

        if (joystick.Direction != Vector2.zero)
        {
            _isMoving = true;
            ChangeAnim(CharacterAnimName.Run);
        }
        else
        {
            _isMoving = false;
            ChangeAnim(CharacterAnimName.Idle);
        }
    }
    protected override void Move()
    {
        if (!_isMoving)
        {
            SetVelocity(0, 0 ,0);
            return;
        }
        
        SetVelocity(joystick.Horizontal, 0, joystick.Vertical);
    }
    
    private void SetVelocity(float x, float y, float z)
    {
        _currentVelocity.Set(x, y, z);
        _currentVelocity.Normalize();
        rb.velocity = _currentVelocity * moveSpeed;
    }
    
}
