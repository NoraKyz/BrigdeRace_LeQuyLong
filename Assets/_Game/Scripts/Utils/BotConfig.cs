using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Utils
{
    [CreateAssetMenu(fileName = "BotConfig", menuName = "Data/Bot Config", order = 2)]
    public class BotConfig : ScriptableObject
    {
        public int moveSpeed;
        public int chanceToFinishPoint;
    }
}