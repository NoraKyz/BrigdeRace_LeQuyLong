using _Game.Character;
using _Game.Pattern.StateMachine;
using UnityEngine;
using Utils;

namespace _Game.Framework.StateMachine
{
    public class IdleState : IState<Enemy>
    {
        private float _timer;
        private const float IdleTime = Constants.TimeToStartGame;

        public void OnEnter(Enemy enemy)
        {
            _timer = 0;
            enemy.ChangeAnim(CharacterAnimName.Idle);
        }

        public void OnExecute(Enemy enemy)
        {
            _timer += Time.deltaTime;
            if (_timer >= IdleTime)
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}
