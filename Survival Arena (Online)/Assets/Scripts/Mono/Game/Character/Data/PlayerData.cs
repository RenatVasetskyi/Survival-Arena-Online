using System;
using UnityEngine;

namespace Mono.Game.Character.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
    }
}