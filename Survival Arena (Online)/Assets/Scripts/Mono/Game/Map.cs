using Business.Game.Interfaces;
using UnityEngine;

namespace Mono.Game
{
    public class Map : MonoBehaviour, IMap
    {
        [SerializeField] private Transform _defenceZone;
        [SerializeField] private float _defenceZoneRadius;
        [SerializeField] private float _castleRadius;
        public Transform DefenceZone => _defenceZone;
        public float DefenceZoneRadius => _defenceZoneRadius;
        public float CastleRadius => _castleRadius;
    }
}
