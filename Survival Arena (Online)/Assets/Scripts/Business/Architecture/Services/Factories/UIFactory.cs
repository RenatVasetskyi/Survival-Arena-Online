using Business.Architecture.Services.Factories.Interfaces;
using Business.Architecture.Services.Interfaces;
using Business.Data.Interfaces;
using Business.Game.UI.Interfaces;
using Business.UI.Interfaces;
using Business.UI.RoomList.Interfaces;
using UnityEngine;
using Zenject;

namespace Business.Architecture.Services.Factories
{
    public class UIFactory : BaseFactory, IUIFactory
    {
        private readonly IGameSettings _gameSettings;
        public ILoadingCurtain LoadingCurtain { get; private set; }
        public IGameView GameView { get; private set; }

        protected UIFactory(IInstantiator instantiator, IAssetProvider assetProvider,
            DiContainer container, IPhotonService photonService, IGameSettings gameSettings) :
            base(instantiator, assetProvider, container, photonService)
        {
            _gameSettings = gameSettings;
        }
        
        public ILoadingCurtain CreateLoadingCurtain()
        {
            if (LoadingCurtain != null)
            {
                LoadingCurtain.Show();
                
                return LoadingCurtain;
            }
            
            GameObject curtain = CreateBaseWithContainer(_gameSettings.GameObjectHolder.LoadingCurtain,
                Vector3.zero, Quaternion.identity, null);
            LoadingCurtain = curtain.GetComponent<ILoadingCurtain>();
            
            LoadingCurtain.Show();
            return LoadingCurtain;
        }
        
        public void CreateMainMenu(Transform parent)
        { 
            CreateBaseWithContainer(_gameSettings.GameObjectHolder.MainMenu,
                Vector3.zero, Quaternion.identity, parent);
        }

        public IRoomButton CreateRoomButton(Transform parent)
        {
            GameObject button = CreateBaseWithContainer(_gameSettings.GameObjectHolder.RoomButton,
                Vector3.zero, Quaternion.identity, null);
            return button.GetComponent<IRoomButton>();
        }
        
        public void MakeLoadingCurtainNull()
        {
            LoadingCurtain = null;
        }
        
        public IGameView CreateGameView(string path, Transform parent)
        {
            return GameView = CreateBaseWithContainer(path, parent).GetComponent<IGameView>();
        }
    }
}