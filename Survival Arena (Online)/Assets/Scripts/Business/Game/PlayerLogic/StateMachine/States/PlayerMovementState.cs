using Business.Game.PlayerLogic.Animation.Interfaces;
using Business.Game.PlayerLogic.Interfaces;
using Business.Game.PlayerLogic.StateMachine.Interfaces;
using UnityEngine;

namespace Business.Game.PlayerLogic.StateMachine.States
{
    public class PlayerMovementState : ICharacterState
    {
        private const float AnimationSpeedMultiplayer = 0.25f;
        
        private readonly IInputController _inputController;
        private readonly Rigidbody _rigidbody;
        private readonly IPlayerAnimator _playerAnimator;
        private readonly float _speed;
        private readonly float _rotationSpeed;

        public PlayerMovementState(IInputController inputController, Rigidbody rigidbody, 
            IPlayerAnimator playerAnimator, ref float speed, float rotationSpeed)
        {
            _inputController = inputController;
            _rigidbody = rigidbody;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _playerAnimator = playerAnimator;
        }
        
        public void Enter()
        {
            _playerAnimator.Walk(_speed * _inputController.CurrentDirection.magnitude);
        }

        public void Exit()
        {
        }

        public void FrameUpdate()
        {
            _playerAnimator.ChangeSpeed(_speed * _inputController
                .CurrentDirection.magnitude * AnimationSpeedMultiplayer);
        }

        public void PhysicsUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 moveDirection =
                new Vector3(_inputController.CurrentDirection.x, 0, _inputController.CurrentDirection.y).normalized;
            _rigidbody.velocity = moveDirection * _speed * _inputController.CurrentDirection.magnitude;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                Time.fixedDeltaTime * _rotationSpeed));
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}