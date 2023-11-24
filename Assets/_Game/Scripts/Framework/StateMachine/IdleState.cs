using _Game.Pattern.StateMachine;
using UnityEngine;
using Utils;

namespace _Game.Framework.StateMachine
{
    public class IdleState : IState<Enemy>
    {
        private float _timer;
        private float _idleTime = Constants.TimeToStartGame;
        public void OnEnter(Enemy enemy)
        {
            _timer = 0;
            enemy.ChangeAnim(CharacterAnimName.Idle);
        }

        public void OnExecute(Enemy enemy)
        {
            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}
