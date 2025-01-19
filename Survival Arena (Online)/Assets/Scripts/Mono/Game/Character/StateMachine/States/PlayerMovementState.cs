using Mono.Game.Character.Animations;
using Mono.Game.Character.Interfaces;
using Mono.Game.Character.StateMachine.Interfaces;
using UnityEngine;

namespace Mono.Game.Character.StateMachine.States
{
    public class PlayerMovementState : ICharacterState
    {
        private const float AnimationSpeedMultiplayer = 0.25f;
        private const float MinMagnitudeToRotate = 0.1f;
        
        private readonly IInputController _inputController;
        private readonly Rigidbody _rigidbody;
        private readonly PlayerAnimator _playerAnimator;
        private readonly float _speed;
        private readonly float _rotationSpeed;

        public PlayerMovementState(IInputController inputController, Rigidbody rigidbody, 
            PlayerAnimator playerAnimator, ref float speed, float rotationSpeed)
        {
            _inputController = inputController;
            _rigidbody = rigidbody;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _playerAnimator = playerAnimator;
        }
        
        public void Enter()
        {
            _playerAnimator.SetSpeed(_speed * AnimationSpeedMultiplayer);
            
            _playerAnimator.PlayWalkAnimation();
        }

        public void Exit()
        {
        }

        public void FrameUpdate()
        {
        }

        public void PhysicsUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(_inputController.CurrentDirection.x, 0, _inputController.CurrentDirection.y).normalized;
            _rigidbody.velocity = moveDirection * _speed;

            if (moveDirection.magnitude > MinMagnitudeToRotate)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                    Time.fixedDeltaTime * _rotationSpeed));
            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}