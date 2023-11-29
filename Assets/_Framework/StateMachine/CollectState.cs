using _Game.Character;
using _Game.Pattern.StateMachine;

namespace _Game.Framework.StateMachine
{
    public class CollectState: IState<Enemy>
    {
        public void OnEnter(Enemy enemy)
        {
            enemy.MoveToRandomBrick();
        }

        public void OnExecute(Enemy enemy)
        {
            
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}