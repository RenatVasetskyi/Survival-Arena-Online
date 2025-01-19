using Business.Game.PlayerLogic.Animation.Interfaces;
using Business.Game.PlayerLogic.StateMachine.Interfaces;

namespace Business.Game.PlayerLogic.StateMachine.States
{
    public class PlayerAttackState : ICharacterState
    {
        private const float AnimationSpeed = 1f;
        
        private readonly IPlayerAnimator _animator;
        private readonly float _damage;

        public PlayerAttackState(IPlayerAnimator animator, ref float damage)
        {
            _animator = animator;
            _damage = damage;
        }
        
        public void Enter()
        {
            _animator.Attack(AnimationSpeed);
        }

        public void Exit()
        {
        }

        public void FrameUpdate()
        {
        }

        public void PhysicsUpdate()
        {
        }
    }
}