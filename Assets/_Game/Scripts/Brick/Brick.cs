using System;
using System.Collections;
using _Game.Utils;
using UnityEngine;

namespace _Game.Brick
{
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
        protected IEnumerator FlyToCharacter(Vector3 targetPosition)
        {
            // TODO: effect fly to player
            yield return new WaitForSeconds(1f);
            OnDespawn();
        }
    }
}
