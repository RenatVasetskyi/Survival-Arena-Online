using Mono.Game.Character.Animations;
using Mono.Game.Character.StateMachine.Interfaces;
using UnityEngine;

namespace Mono.Game.Character.StateMachine.States
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
            
            _playerAnimator.SetSpeed(AnimationSpeed);
            _playerAnimator.PlayIdleAnimation();
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