using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{   
    [SerializeField] private FloatingJoystick joystick;

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += new Vector3(joystick.Horizontal, 0, joystick.Vertical) * Time.deltaTime * speed;
    }
}
