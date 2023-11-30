using System;
using _Framework;
using _Game.Utils;
using UnityEngine;
using Utils;

namespace _Game.Brick
{
    public class BridgeBrick : Brick
    {
        private void OnEnable()
        {
            ChangeColor(ColorType.Gray);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
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
