using System.Collections.Generic;
using System.Linq;

namespace _Game.Utils
{
    public static class Utilities
    {
        // random thu tu 1 list
        public static List<T> RandomList<T>(List<T> list, int amout)
        {
            return list.OrderBy(d => System.Guid.NewGuid()).Take(amout).ToList();
        }
        
        // lay ket qua theo ty le xac suat
        public static bool Chance(int rand, int max = 100)
        {
            return UnityEngine.Random.Range(0, max) < rand;
        }
        
        // random 1 gia tri enum trong 1 kieu enum
        private static System.Random random = new System.Random();
        public static T RandomEnumValue<T>()
        {
            var v = System.Enum.GetValues(typeof(T));
            return (T) v.GetValue(random.Next(v.Length));
        }
    }
}