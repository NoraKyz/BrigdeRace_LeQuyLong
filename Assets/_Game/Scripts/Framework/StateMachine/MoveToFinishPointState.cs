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
            if (!enemy.CheckStair(enemy.transform.position + enemy.Veclocity * Time.deltaTime))
            {
                enemy.ChangeState(enemy.CollectState);
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}