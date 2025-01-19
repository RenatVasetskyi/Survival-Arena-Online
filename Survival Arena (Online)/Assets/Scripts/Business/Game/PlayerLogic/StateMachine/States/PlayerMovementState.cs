using Business.Game.PlayerLogic.Animations;
using Business.Game.PlayerLogic.Interfaces;
using Business.Game.PlayerLogic.StateMachine.Interfaces;
using UnityEngine;

namespace Business.Game.PlayerLogic.StateMachine.States
{
    public class PlayerMovementState : ICharacterState
    {
        private const float AnimationSpeedMultiplayer = 0.2f;
        
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
            _playerAnimator.Walk(_speed * AnimationSpeedMultiplayer);
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
            Vector3 moveDirection =
                new Vector3(_inputController.CurrentDirection.x, 0, _inputController.CurrentDirection.y).normalized;
            _rigidbody.velocity = moveDirection * _speed;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                Time.fixedDeltaTime * _rotationSpeed));
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}