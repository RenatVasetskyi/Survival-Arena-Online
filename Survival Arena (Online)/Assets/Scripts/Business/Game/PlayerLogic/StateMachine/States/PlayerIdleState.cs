using Business.Game.PlayerLogic.Animations;
using Business.Game.PlayerLogic.StateMachine.Interfaces;
using UnityEngine;

namespace Business.Game.PlayerLogic.StateMachine.States
{
    public class PlayerIdleState : ICharacterState
    {
        private const float AnimationSpeed = 1f;
        
        private readonly PlayerAnimator _playerAnimator;
        private readonly Rigidbody _rigidbody;

        public PlayerIdleState(PlayerAnimator playerAnimator, Rigidbody rigidbody)
        {
            _playerAnimator = playerAnimator;
            _rigidbody = rigidbody;
        }
        
        public void Enter()
        {
            StopRigidbody();
            
            _playerAnimator.Idle(AnimationSpeed);
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
        
        private void StopRigidbody()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}