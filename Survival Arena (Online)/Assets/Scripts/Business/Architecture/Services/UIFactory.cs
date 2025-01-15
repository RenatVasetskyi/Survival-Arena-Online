using System.Threading.Tasks;
using Business.Architecture.Services.Interfaces;
using Business.Data.Interfaces;
using Business.UI.Interfaces;
using Business.UI.RoomList.Interfaces;
using UnityEngine;
using Zenject;

namespace Business.Architecture.Services
{
    public class UIFactory : BaseFactory, IUIFactory
    {
        private readonly IGameSettings _gameSettings;
        public ILoadingCurtain LoadingCurtain { get; private set; }

        protected UIFactory(IInstantiator instantiator, IAssetProvider assetProvider, DiContainer container,
            IPhotonService photonService, IGameSettings gameSettings) :
            base(instantiator, assetProvider, container, photonService)
        {
            _gameSettings = gameSettings;
        }
        
        public async Task CreateLoadingCurtain()
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
        
        public async Task CreateMainMenu(Transform parent)
        { 
            await CreateAddressableWithContainer(_gameSettings.AddressableAssets
                .MainMenu, Vector3.zero, Quaternion.identity, parent);
        }

        public void MakeLoadingCurtainNull()
        {
            LoadingCurtain = null;
        }

        public async Task<IRoomButton> CreateRoomButton(Transform parent)
        {
            GameObject roomView = await CreateAddressableWithContainer
                (_gameSettings.AddressableAssets.LoadingCurtain, Vector3.zero, Quaternion.identity, parent);
            return roomView.GetComponent<IRoomButton>();
        }
    }
}