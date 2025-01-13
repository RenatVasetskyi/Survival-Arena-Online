using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Business.Data
{
    [Serializable]
    public class AddressableAssets
    {
        [SerializeField] private AssetReferenceGameObject _loadingCurtain;
        [SerializeField] private AssetReferenceGameObject _roomButton;
        [SerializeField] private AssetReferenceGameObject _mainMenu;
        public AssetReferenceGameObject LoadingCurtain => _loadingCurtain;
        public AssetReferenceGameObject RoomButton => _roomButton;
        public AssetReferenceGameObject MainMenu => _mainMenu; 
    }
}