using Business.Game.Spawn.Interfaces;
using UnityEngine;

namespace Mono.Game
{
    public class Map : MonoBehaviour, IMap
    {
        [SerializeField] private Transform _spawnPoint;

        public Transform SpawnPoint => _spawnPoint; 
    }
}
