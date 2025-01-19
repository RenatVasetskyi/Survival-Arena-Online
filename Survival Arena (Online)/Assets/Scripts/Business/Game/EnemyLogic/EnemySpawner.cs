using System.Collections;
using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Game.EnemyLogic.Interfaces;
using UnityEngine;

namespace Business.Game.EnemyLogic
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPhotonService _photonService;

        private Coroutine _spawnCoroutine;

        public EnemySpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner,
            IPhotonService photonService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _photonService = photonService;
        }

        ~EnemySpawner()
        {
            if (_spawnCoroutine != null) 
                _coroutineRunner.StopCoroutine(_spawnCoroutine);
        }

        public void Spawn(Transform center, Quaternion rotation, Transform parent)
        { 
            _spawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(center, rotation, parent));
        }

        private IEnumerator SpawnEnemies(Transform center, Quaternion rotation, Transform parent)
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / _photonService.PlayersInRoom);

                _gameFactory.CreateEnemy(center, rotation, parent);
            }
        }
    }
}