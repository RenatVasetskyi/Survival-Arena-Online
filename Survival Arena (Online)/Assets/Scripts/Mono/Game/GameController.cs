using Business.Architecture.Services.Interfaces;
using Business.Data.Interfaces;
using Business.Game.Spawn;
using Business.Game.Spawn.Interfaces;
using UnityEngine;
using Zenject;

namespace Mono.Game
{
    public class GameController : MonoBehaviour
    {
        private readonly IPlayerSpawner _playerSpawner = new PlayerSpawner();
        
        [SerializeField] private Transform _middleSpawnPoint; 
        
        private IEventService _eventService;
        private IGameSettings _gameSettings;
        
        [Inject]
        public void Inject(IEventService eventService, IGameSettings gameSettings)
        {
            _eventService = eventService;
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            _eventService.OnJoinedRoom += SpawnPlayer;
        }

        private void OnDestroy()
        {
            _eventService.OnJoinedRoom -= SpawnPlayer;
        }

        private void SpawnPlayer()
        {
            _playerSpawner.SpawnPlayerInRange(_middleSpawnPoint, _gameSettings.PlayerSpawnRange);
        }
    }
}