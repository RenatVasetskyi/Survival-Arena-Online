using Business.Data.Interfaces;
using UnityEngine;

namespace Business.Data
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Create Settings Holder/Game Settings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private AddressableAssets _addressableAssets;
        [SerializeField] private float _playerSpawnRange;
        public AddressableAssets AddressableAssets => _addressableAssets;
        public float PlayerSpawnRange => _playerSpawnRange;
    }
}
