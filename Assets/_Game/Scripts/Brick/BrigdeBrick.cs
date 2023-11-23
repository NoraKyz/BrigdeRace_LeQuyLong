using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigdeBrick : Brick
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();

            if (character.colorType != colorType && character.BrickAmount > 0)
            {
                character.RemoveBrick();
                ChangeColor(character.colorType);
            }
        }
    }
}
