using _Framework.Pool.Scripts;
using _Game.Manager;
using UnityEngine;

namespace _Game.Camera
{
    public class CameraFollow : GameUnit
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
        
        private void LateUpdate()
        {
            if (!GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(TF.position, desiredPosition, smoothSpeed);
            TF.position = smoothedPosition;
        }
        
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    
        // TODO: increasing offset when player's brick count increase
        
    }
}
