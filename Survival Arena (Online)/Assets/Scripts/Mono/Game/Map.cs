using Business.Game.Interfaces;
using UnityEngine;

namespace Mono.Game
{
    public class Map : MonoBehaviour, IMap
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _defenceZone;
        public Transform SpawnPoint => _spawnPoint;
        public Transform DefenceZone => _defenceZone;
    }
}
