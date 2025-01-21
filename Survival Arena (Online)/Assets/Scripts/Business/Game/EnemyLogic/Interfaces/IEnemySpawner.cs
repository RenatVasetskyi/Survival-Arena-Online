using UnityEngine;

namespace Business.Game.EnemyLogic.Interfaces
{
    public interface IEnemySpawner
    {
        void Spawn(Transform center, Quaternion rotation, Transform parent);
        void RemoveEnemy(IEnemy enemy);
    }
}