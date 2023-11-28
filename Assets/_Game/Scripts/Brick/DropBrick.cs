using System.Collections;
using UnityEngine;
using Cache = _Framework.Cache;

namespace _Game.Brick
{
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
                Character.Character character = Cache.GetCharacter(other);
        
                if (!character.isFalling)
                {
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
}
