using System;
using UnityEngine;
using Cache = _Framework.Cache;

namespace _Game.Brick
{
    public class PlatformBrick : Brick
    {
        public event Action<PlatformBrick> OnDespawnEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache.GetCharacter(other);

                if (character.colorType == colorType && !character.isFalling)
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
