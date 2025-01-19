using System;
using UnityEngine;

namespace Business.Game.PlayerLogic.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _damage;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public float Damage => _damage;
    }
}