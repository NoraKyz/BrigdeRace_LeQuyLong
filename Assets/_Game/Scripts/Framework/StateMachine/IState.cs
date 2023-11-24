namespace _Game.Framework.StateMachine
{
    public interface IState<T>
    {
        void OnEnter(T t);
        void OnExecute(T t);
        void OnExit(T t);
    }
}
