using Business.Game.Interfaces;
using Mono.Game.Character.Animations;
using Mono.Game.Character.Data;
using Mono.Game.Character.Interfaces;
using Mono.Game.Character.StateMachine;
using Mono.Game.Character.StateMachine.Interfaces;
using Mono.Game.Character.StateMachine.States;
using UnityEngine;

namespace Mono.Game.Character
{
    public class Player : MonoBehaviour, IPlayer
    {
        private readonly ICharacterStateMachine _stateMachine = new CharacterStateMachine();

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerData _data;

        private IInputController _inputController;
        private PlayerAnimator _playerAnimator;
        private Camera _camera;
        
        private float _currentSpeed;

        public void Initialize(IInputController inputController, Camera camera)
        {
            _inputController = inputController;
            _playerAnimator = new PlayerAnimator(_animator);
            _currentSpeed = _data.Speed;
            _camera = camera;
            
            StateFactory stateFactory = new StateFactory
                (_stateMachine, _inputController, _rigidbody, _playerAnimator, _data, _currentSpeed);

            Subscribe();
            
            EnterIdleState();
        }
        
        private void Update()
        {
            _stateMachine.ActiveState?.FrameUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.ActiveState?.PhysicsUpdate();
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
        
        private void Subscribe()
        {
            _inputController.OnInputActivated += EnterMovementState;
            _inputController.OnInputDeactivated += EnterIdleState;
        }

        private void UnSubscribe()
        {
            _inputController.OnInputActivated -= EnterMovementState;
            _inputController.OnInputDeactivated -= EnterIdleState;
        }

        private void EnterMovementState()
        {
            _stateMachine.EnterState<PlayerMovementState>();
        }

        private void EnterIdleState()
        {
            _stateMachine.EnterState<PlayerIdleState>();
        }

        private class StateFactory
        {
            private readonly ICharacterStateMachine _stateMachine;
            private readonly IInputController _inputController;
            private readonly Rigidbody _rigidbody;
            private readonly PlayerAnimator _playerAnimator;
            private readonly PlayerData _data;
            private float _speed;

            public StateFactory(ICharacterStateMachine stateMachine, IInputController inputController, 
                Rigidbody rigidbody, PlayerAnimator playerAnimator, PlayerData data, float speed)
            {
                _stateMachine = stateMachine;
                _inputController = inputController;
                _rigidbody = rigidbody;
                _playerAnimator = playerAnimator;
                _data = data;
                _speed = speed;

                CreateStates();
            }

            private void CreateStates()
            {
                CreatePlayerIdleState();
                CreatePlayerMovementState();
            }

            private void CreatePlayerIdleState()
            {
                _stateMachine.AddState<PlayerIdleState>(new PlayerIdleState(_playerAnimator, _rigidbody));
            }

            private void CreatePlayerMovementState()
            {
                _stateMachine.AddState<PlayerMovementState>(new PlayerMovementState
                    (_inputController, _rigidbody, _playerAnimator, ref _speed, _data.Speed));
            }
        }
    }
}