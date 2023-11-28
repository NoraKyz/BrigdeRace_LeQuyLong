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
            if (!enemy.CheckStair(enemy.NextPosition))
            {
                enemy.NotEnoughBrick();
            }

            enemy.CheckGate();
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}