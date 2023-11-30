using System;
using _Framework;
using _Game.Utils;
using UnityEngine;

namespace _Game.Brick
{
    public class PlatformBrick : Brick
    {
        public event Action<PlatformBrick> OnDespawnEvent;
        
        private void OnEnable()
        {
            OnDespawnEvent = null;
        }
        
        protected override void OnDespawn()
        {
            base.OnDespawn();
            OnDespawnEvent?.Invoke(this);
            OnDespawnEvent = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);

                if (ColorType == character.ColorType && !character.IsFalling)
                {
                    OnDespawn();
                    character.AddBrick();
                }
            }
        }
    }
}
