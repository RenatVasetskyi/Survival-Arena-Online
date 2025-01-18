using Business.Data.Interfaces;
using UnityEngine;

namespace Business.Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private AddressableAssets _addressableAssets;
        [SerializeField] private GameObjectHolder _gameObjectHolder;
        [SerializeField] private float _playerMinSpawnRange;
        [SerializeField] private float _playerMaxSpawnRange;
        public AddressableAssets AddressableAssets => _addressableAssets;
        public GameObjectHolder GameObjectHolder => _gameObjectHolder;
        public float PlayerMinSpawnRange => _playerMinSpawnRange;
        public float PlayerMaxSpawnRange => _playerMaxSpawnRange;
    }
}
