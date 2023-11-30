using _Framework.Pool.Scripts;
using UnityEngine;
using Utils;

namespace _Game.Utils
{
    public class ObjectColor : GameUnit
    {
        [Header("Config Color")]
        [SerializeField] private ColorType colorType;
        [SerializeField] private ColorData colorData;
        [SerializeField] private Renderer render;
        public ColorType ColorType => colorType;
        
        protected virtual void OnInit()
        {
            ChangeColor(ColorType);
        }
    
        public void ChangeColor(ColorType color)
        {
            colorType = color;
            render.material = colorData.GetMaterial(color);
        }
    }
}
