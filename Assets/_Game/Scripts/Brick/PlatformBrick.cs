using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrick : Brick
{
    public event Action<PlatformBrick> OnDespawnEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();

            if (character.colorType == colorType)
            {
                character.AddBrick();
                OnDespawn();
            }
        }
    }
    
    protected override void OnDespawn()
    {
        base.OnDespawn();
        OnDespawnEvent?.Invoke(this);
    }
}
