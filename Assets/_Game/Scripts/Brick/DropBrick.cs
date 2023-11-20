using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick : Brick
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            character.AddBrick();

            OnDespawn();
        }
    }
}
