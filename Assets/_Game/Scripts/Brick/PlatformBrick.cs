using System;
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
                Character.Character character = other.GetComponent<Character.Character>();

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
