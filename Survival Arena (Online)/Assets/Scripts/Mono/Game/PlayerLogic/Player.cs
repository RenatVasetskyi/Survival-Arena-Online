using Business.Game.Interfaces;
using Business.Game.PlayerLogic.Animation;
using Business.Game.PlayerLogic.Animation.Interfaces;
using Business.Game.PlayerLogic.Data;
using Business.Game.PlayerLogic.Interfaces;
using Business.Game.PlayerLogic.StateMachine;
using Business.Game.PlayerLogic.StateMachine.Interfaces;
using Business.Game.PlayerLogic.StateMachine.States;
using Photon.Pun;
using UnityEngine;

namespace Mono.Game.PlayerLogic
{
    public class Player : MonoBehaviour, IPlayer
    {
        private readonly ICharacterStateMachine _stateMachine = new CharacterStateMachine();

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerData _data;
        private IPlayerAnimator _playerAnimator;

        private IInputController _inputController;
        
        private float _currentSpeed;

        public void Initialize(IInputController inputController)
        {
            _inputController = inputController;
            _currentSpeed = _data.Speed;

            _playerAnimator = new PlayerAnimator(_animator, GetComponent<PhotonView>());
            
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

        private sealed class StateFactory
        {
            private readonly ICharacterStateMachine _stateMachine;
            private readonly IInputController _inputController;
            private readonly Rigidbody _rigidbody;
            private readonly IPlayerAnimator _playerAnimator;
            private readonly PlayerData _data;
            private float _speed;

            public StateFactory(ICharacterStateMachine stateMachine, IInputController inputController, 
                Rigidbody rigidbody, IPlayerAnimator playerAnimator, PlayerData data, float speed)
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