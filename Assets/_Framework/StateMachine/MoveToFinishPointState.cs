using _Game.Character;
using UnityEngine;
using Utils;

namespace _Game.Framework.StateMachine
{
    public class MoveToFinishPointState: IState<Enemy>
    {
        public void OnEnter(Enemy enemy)
        {
            enemy.MoveToFinishPos();
        }

        public void OnExecute(Enemy enemy)
        {
            if (!enemy.CheckStair(enemy.NextPosition) || enemy.IsDestination)
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}