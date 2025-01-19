using Business.Game.PlayerLogic.Animation.Interfaces;
using Business.Game.PlayerLogic.StateMachine.Interfaces;
using Mono.Game.PlayerLogic;
using UnityEngine;

namespace Business.Game.PlayerLogic.StateMachine.States
{
    public class PlayerIdleState : ICharacterState
    {
        private const float AnimationSpeed = 1f;
        
        private readonly IPlayerAnimator _playerAnimator;
        private readonly Rigidbody _rigidbody;

        public PlayerIdleState(IPlayerAnimator playerAnimator, Rigidbody rigidbody)
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