using System.Collections;
using _Framework;
using _Game.Utils;
using UnityEngine;

namespace _Game.Brick
{
    public class DropBrick : Brick
    {
        private bool _isTakeAble;

        private void OnEnable()
        {
            _isTakeAble = false;
            StartCoroutine(TakeAbleCd());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isTakeAble)
            {
                return;
            }
        
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);
        
                if (!character.IsFalling)
                {
                    OnDespawn();
                    character.AddBrick();
                }
            }
        }
    
        private IEnumerator TakeAbleCd()
        {
            yield return new WaitForSeconds(1f);
            _isTakeAble = true;
        }
    }
}
