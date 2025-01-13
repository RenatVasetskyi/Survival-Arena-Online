using Business.Architecture.Services.Interfaces;
using Business.Data;
using Business.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace Business.Architecture.Services
{
    public class UIFactory : BaseFactory, IUIFactory
    {
        private readonly GameSettings _gameSettings;
        public ILoadingCurtain LoadingCurtain { get; private set; }
        
        public UIFactory(DiContainer container, IAssetProvider assetProvider, GameSettings gameSettings)
            : base(container, assetProvider)
        {
            _gameSettings = gameSettings;
        }

        public async void CreateLoadingCurtain()
        {
            if (LoadingCurtain != null)
            {
                LoadingCurtain.Show();
                
                return;
            }
            
            GameObject curtain = await CreateAddressableWithContainer
                (_gameSettings.AddressableAssets.LoadingCurtain, Vector3.zero, Quaternion.identity, null);
            LoadingCurtain = curtain.GetComponent<ILoadingCurtain>();
            LoadingCurtain.Show();  
        }
    }
}