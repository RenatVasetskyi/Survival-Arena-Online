using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data;
using Business.Data.Interfaces;
using Business.Game.Spawn.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Business.Game.Spawn
{
    public class PlayerSpawner : IPlayerSpawner
    {
        private readonly IFactory _factory;
        private readonly IGameSettings _gameSettings;

        public PlayerSpawner(IFactory factory, IGameSettings gameSettings)
        {
            _factory = factory;
            _gameSettings = gameSettings;
        }
        
        public void SpawnPlayerInRange(Transform middlePoint, Transform parent)
        {
            Vector2 randomPoint = Random.insideUnitCircle * _gameSettings.PlayerSpawnRange;
            
            Vector3 spawnPosition = new Vector3(
                middlePoint.position.x + randomPoint.x,
                middlePoint.position.y + 1,
                middlePoint.position.z + randomPoint.y
            );

            _factory.CreateWithPhoton<PhotonView>(AssetPath.Player,
                spawnPosition, Quaternion.identity, parent);
        }
    }
}