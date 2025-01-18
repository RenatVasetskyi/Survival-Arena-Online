using Business.Data.Interfaces;
using Business.Game.Spawn;
using Business.Game.Spawn.Interfaces;
using UnityEngine;
using Zenject;
using IFactory = Business.Architecture.Services.Interfaces.IFactory;

namespace Mono.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _playerHolder; 
        [SerializeField] private Transform _middleSpawnPoint; 
        
        private IPlayerSpawner _playerSpawner;
        
        [Inject]
        public void Inject(IGameSettings gameSettings, IFactory factory)
        {
            _playerSpawner = new PlayerSpawner(factory, gameSettings);
        }

        private void Awake()
        { 
            _playerSpawner.SpawnPlayerInRange(_middleSpawnPoint, _playerHolder);
        }
    }
}