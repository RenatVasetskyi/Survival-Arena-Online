using Business.Game.Interfaces;
using UnityEngine;

namespace Mono.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        private const float MinMagnitudeToRotate = 0.1f;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;

        private Joystick _joystick;
        private bool _initialized;

        public void Initialize(Joystick joystick)
        {
            _joystick = joystick;
            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (!_initialized)
                return;

            Move();
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y).normalized;
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