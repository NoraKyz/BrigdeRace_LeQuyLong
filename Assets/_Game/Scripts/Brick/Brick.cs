using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Utils;
using UnityEngine;
using Utils;

public class Brick : ObjectColor
{
    protected void Start()
    {
        OnInit();
    }
    
    protected virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    protected virtual IEnumerator FlyToCharacter(Vector3 targetPosition)
    {
        // TODO: effect fly to player
        yield return new WaitForSeconds(1f);
        OnDespawn();
    }
}
