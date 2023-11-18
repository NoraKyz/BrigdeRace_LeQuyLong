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
    
    private Vector2 moveDirection = Vector2.zero;
    private Vector3 _currentVelocity = Vector3.zero;
    
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
        
        moveDirection = joystick.Direction;
        
        if (moveDirection != Vector2.zero)
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
            SetVelocity(0,0,0);
            return;
        } 
        
        SetVelocity(moveDirection.x, rb.velocity.y, moveDirection.y);
    }
    private void SetVelocity(float x, float y, float z)
    {
        _currentVelocity.Set(x, y, z);
        _currentVelocity.Normalize();
        rb.velocity = _currentVelocity * speed;
    }
}
