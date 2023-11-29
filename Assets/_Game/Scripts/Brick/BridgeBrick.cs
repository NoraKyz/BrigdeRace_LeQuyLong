using _Framework;
using UnityEngine;

namespace _Game.Brick
{
    public class BridgeBrick : Brick
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache<Character.Character>.GetScript(other);

                if (character.ColorType != ColorType && character.BrickAmount > 0)
                {
                    character.RemoveBrick();
                    ChangeColor(character.ColorType);
                }
            }
        }
    }
}
