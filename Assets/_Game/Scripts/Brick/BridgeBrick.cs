using _Framework;
using _Game.Utils;
using UnityEngine;
using Utils;

namespace _Game.Brick
{
    public class BridgeBrick : Brick
    {
        private void OnDisable()
        {
            ChangeColor(ColorType.Gray);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);

                if (ColorType != character.ColorType && character.BrickAmount > 0)
                {
                    ChangeColor(character.ColorType);
                    character.RemoveBrick();
                }
            }
        }
    }
}
