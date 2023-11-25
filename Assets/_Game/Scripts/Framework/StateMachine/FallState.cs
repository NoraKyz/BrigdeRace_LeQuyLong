using _Game.Framework.StateMachine;
using UnityEngine;
using Utils;

namespace _Game.Pattern.StateMachine
{
    public class FallState: IState<Enemy>
    {
        private float _timer;
        private float _stunTime = Constants.StunTime;
        public void OnEnter(Enemy enemy)
        {
            _timer = 0;
            enemy.isFalling = true;
            enemy.StopMove();
            enemy.ChangeAnim(CharacterAnimName.Fall);
        }

        public void OnExecute(Enemy enemy)
        {
            _timer += Time.deltaTime;
            if (_timer >= _stunTime)
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            enemy.isFalling = false;
        }
    }
}