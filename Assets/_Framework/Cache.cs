using System.Collections.Generic;
using _Game.Character;
using UnityEngine;

namespace _Framework
{
    public class Cache 
    {
        private static Dictionary<Collider, Character> _dictCharacters = new Dictionary<Collider, Character>();
        
        public static Character GetCharacter(Collider collider)
        {
            if (_dictCharacters.TryGetValue(collider, out var character))
            {
                return character;
            }
            else
            {
                Character collectItems = collider.GetComponent<Character>();
                _dictCharacters.Add(collider, collectItems);
                return collectItems;
            }
        }
            
    }
}