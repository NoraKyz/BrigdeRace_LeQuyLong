using _Game.Character;
namespace _Framework.StateMachine
{
    public class CollectState: IState<Enemy>
    {
        public void OnEnter(Enemy enemy)
        {
            enemy.MoveToRandomBrick();
        }

        public void OnExecute(Enemy enemy)
        {
            if (enemy.IsDestination)
            {
                enemy.RandomChanceMoveToFinishPos();
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
    }
}