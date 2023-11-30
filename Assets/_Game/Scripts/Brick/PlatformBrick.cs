using System;
using _Framework;
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
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache<Character.Character>.GetScript(other);

                if (character.ColorType == ColorType && !character.IsFalling)
                {
                    OnDespawn();
                    character.AddBrick();
                }
            }
        }
    
        protected override void OnDespawn()
        {
            base.OnDespawn();
            OnDespawnEvent?.Invoke(this);
        }
    }
}
