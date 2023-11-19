using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public enum ColorType
    {
        None = 0,
        Blue = 1,
        Purple = 2,
        Red = 3,
        Orange = 4,
        Pink = 5,
        Green = 6,
        Gray = 7
    }
    
    [CreateAssetMenu(fileName = "ColorData", menuName = "Data/Color Data", order = 1)]
    public sealed class ColorData : ScriptableObject
    {
        public List<Material> materials = new List<Material>();
        
        public Material GetMaterial(ColorType colorType)
        {
            return materials[(int) colorType];
        }
    }
}