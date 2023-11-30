using _Game.Character;
using _Game.Framework.StateMachine;
using UnityEngine;
using Utils;

namespace _Game.Pattern.StateMachine
{
    public class FallState: IState<Enemy>
    {
        private float _timer;
        private const float StunTime = Constants.StunTime;
        
        public void OnEnter(Enemy enemy)
        {
            _timer = 0;
            
            enemy.StopMove();
        }

        public void OnExecute(Enemy enemy)
        {
            _timer += Time.deltaTime;
            if (_timer >= StunTime)
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}