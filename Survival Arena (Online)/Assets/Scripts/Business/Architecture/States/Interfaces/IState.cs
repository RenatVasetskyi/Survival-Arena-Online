namespace Business.Architecture.States.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}