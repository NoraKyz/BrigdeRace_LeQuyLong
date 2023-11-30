using _Framework;
using _Game.Utils;
using UnityEngine;

namespace _Game.Map
{
    public class WinPos : MonoBehaviour
    {
        [SerializeField] private Transform top1Pos;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetScript(other);
            
                character.OnWinPos();
                StartCoroutine(character.MovePosition(top1Pos.position, 0.5f));
            }
        }
    }
}
