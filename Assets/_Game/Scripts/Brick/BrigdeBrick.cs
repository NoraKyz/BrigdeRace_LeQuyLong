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

            if (character.ColorType != colorType && character.HasBrick)
            {
                character.RemoveBrick();
                ChangeColor(character.ColorType);
            }
        }
    }
}