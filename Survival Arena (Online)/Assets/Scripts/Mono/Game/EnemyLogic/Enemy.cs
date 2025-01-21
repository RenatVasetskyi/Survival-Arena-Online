using Business.Game.EnemyLogic.Interfaces;
using UnityEngine;

namespace Mono.Game.EnemyLogic
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private IEnemySpawner _enemySpawner;
        
        public void Initialize(IEnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        private void Die()
        {
            _enemySpawner.RemoveEnemy(this);
        }
    }
}