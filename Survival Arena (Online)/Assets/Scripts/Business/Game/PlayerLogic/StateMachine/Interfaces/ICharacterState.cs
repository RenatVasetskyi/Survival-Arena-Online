namespace Business.Game.PlayerLogic.StateMachine.Interfaces
{
    public interface ICharacterState
    {
        void Enter();
        void Exit();
        void FrameUpdate();
        void PhysicsUpdate();
    }
}