using System;
using _Framework;
using UnityEngine;

namespace _Game.Brick
{
    public class PlatformBrick : Brick
    {
        public event Action<PlatformBrick> OnDespawnEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache<Character.Character>.GetScript(other);

                if (character.ColorType == colorType && !character.IsFalling)
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
