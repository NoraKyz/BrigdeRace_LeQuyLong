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
        
        protected ColorType colorType;
        
        public ColorType ColorType => colorType;

        protected virtual void OnInit()
        {
            ChangeColor(colorType);
        }
    
        public void ChangeColor(ColorType colorType)
        {
            this.colorType = colorType;
            renderer.material = colorData.GetMaterial(colorType);
        }
    }
}
