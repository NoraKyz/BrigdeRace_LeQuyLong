using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick : Brick
{
    private bool _isTakeAble;

    private void OnEnable()
    {
        _isTakeAble = false;
        StartCoroutine(TakeAble());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTakeAble)
        {
            return;
        }
        
        if (other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
        
            if (!character.isFalling)
            {
                StopCoroutine(TakeAble());
                OnDespawn();
                character.AddBrick();
            }
        }
    }
    
    private IEnumerator TakeAble()
    {
        yield return new WaitForSeconds(1f);
        _isTakeAble = true;
    }
}
