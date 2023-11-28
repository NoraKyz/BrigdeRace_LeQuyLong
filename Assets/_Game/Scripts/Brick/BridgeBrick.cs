using UnityEngine;
using Cache = _Framework.Cache;

namespace _Game.Brick
{
    public class BridgeBrick : Brick
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache.GetCharacter(other);

                if (character.colorType != colorType && character.BrickAmount > 0)
                {
                    character.RemoveBrick();
                    ChangeColor(character.colorType);
                }
            }
        }
    }
}
