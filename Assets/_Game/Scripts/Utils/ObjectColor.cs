using _Framework.Pool.Scripts;
using UnityEngine;
using Utils;

namespace _Game.Utils
{
    public class ObjectColor : GameUnit
    {
        [Header("Config Color")]
        [SerializeField] private ColorData colorData;
        [SerializeField] private Renderer renderer;
        
        public ColorType ColorType { get; private set; }

        protected virtual void OnInit()
        {
            ChangeColor(ColorType);
        }
    
        public void ChangeColor(ColorType colorType)
        {
            ColorType = colorType;
            renderer.material = colorData.GetMaterial(colorType);
        }
    }
}
