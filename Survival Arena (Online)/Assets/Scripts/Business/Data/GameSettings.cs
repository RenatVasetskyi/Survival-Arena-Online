using Business.Data.Interfaces;
using UnityEngine;

namespace Business.Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private AddressableAssets _addressableAssets;
        [SerializeField] private GameObjectHolder _gameObjectHolder;
        [SerializeField] private float _enemyMinSpawnRange;
        [SerializeField] private float _enemyMaxSpawnRange;
        [SerializeField] private int _maxEnemies;
        public AddressableAssets AddressableAssets => _addressableAssets;
        public GameObjectHolder GameObjectHolder => _gameObjectHolder;
        public float EnemyMinSpawnRange => _enemyMinSpawnRange;
        public float EnemyMaxSpawnRange => _enemyMaxSpawnRange;
        public int MaxEnemies => _maxEnemies;
    }
}
