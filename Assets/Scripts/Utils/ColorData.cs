using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
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