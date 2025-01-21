using System.Collections;
using System.Collections.Generic;
using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data.Interfaces;
using Business.Game.EnemyLogic.Interfaces;
using UnityEngine;

namespace Business.Game.EnemyLogic
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPhotonService _photonService;
        private readonly IGameSettings _gameSettings;
        private readonly List<IEnemy> _enemies = new();

        private Coroutine _spawnCoroutine;

        public EnemySpawner(IGameFactory gameFactory, ICoroutineRunner coroutineRunner,
            IPhotonService photonService, IGameSettings gameSettings)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _photonService = photonService;
            _gameSettings = gameSettings;
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

        public void RemoveEnemy(IEnemy enemy)
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);
        }

        private IEnumerator SpawnEnemies(Transform center, Quaternion rotation, Transform parent)
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / _photonService.PlayersInRoom);
                yield return new WaitUntil(() => _enemies.Count < _gameSettings.MaxEnemies);

                IEnemy enemy = _gameFactory.CreateEnemy(center, rotation, parent);
                enemy.Initialize(this);
                _enemies.Add(enemy);
            }
        }
    }
}